/* Same-origin Agent workspace. Query parameters never create an identity. */
'use strict';
const $=id=>document.getElementById(id);
const state={mobile:'',incoming:'',agentId:'',session:null,csrf:'',customer:null,page:1,historySize:5,fields:[],version:null,key:null,payload:null,busy:false,services:[],load:0};
const text=(id,value)=>{$(id).textContent=value??'—';};
function notice(message,success=false){text('notice',message);$('notice').classList.toggle('success',success);$('notice').hidden=!message;}
async function csrf(){const data=await api('/api/v1/auth/csrf');state.csrf=data.token;}
async function api(url,body){
 const response=await fetch(url,{method:body===undefined?'GET':'POST',credentials:'same-origin',cache:'no-store',headers:body===undefined?{}:{'Content-Type':'application/json','X-CSRF-TOKEN':state.csrf},body:body===undefined?undefined:JSON.stringify(body)});
 let payload;try{payload=await response.json();}catch{throw Error('The API returned an unreadable response.');}
 if(!response.ok||payload.success===false){
  if(response.status===401){state.session=null;$('session-panel').hidden=false;$('workspace').hidden=true;}
  throw Error((payload.message||`Request failed (${response.status}).`)+(payload.correlationId?` Reference: ${payload.correlationId}`:''));
 }
 return Object.hasOwn(payload,'data')?payload.data:payload;
}
function rows(data){return data?.rows1??[];}
function validateLaunch(mobile,agentId){return /^\+?[0-9]{8,20}$/.test(mobile)&&agentId.length<=100&&!/[\x00-\x1f]/.test(agentId);}
function clearCustomer(){state.customer=null;state.page=1;state.load++;text('customer-name','Select a customer');text('customer-reference','—');text('customer-mobile','—');text('customer-cif','—');text('customer-initials','—');text('ticket-count','0');text('profile-note','');$('customer-options').replaceChildren();$('ticket-rows').replaceChildren();$('create').disabled=true;for(const system of ['core','card','ib']){text(system+'-status','Not loaded');text(system+'-identifier','—');$(system+'-details').disabled=true;$(system+'-details').onclick=null;}}
async function verify(){
 clearCustomer();$('workspace').hidden=true;
 try{
  const session=await api('/api/v1/auth/session');
  $('logout').hidden=false;
  if(session.capability!=='AGENT')throw Error('This workspace requires an Agent session.');
  if(state.agentId&&session.id!==state.agentId)throw Error(`Launch Agent ID does not match signed-in Agent ${session.id}. Use the correct launch URL or sign out.`);
  state.session=session;await csrf();text('agent-name',session.name);text('agent-id',session.id);$('logout').hidden=false;$('session-panel').hidden=true;$('workspace').hidden=false;text('launch-mobile',state.incoming||state.mobile);notice('');
  await loadCustomers();
 }catch(error){state.session=null;$('session-panel').hidden=false;text('session-message',error.message);notice('Verify your Agent session to load customer information.');}
}
async function loadCustomers(){
 clearCustomer();const current=state.load;
 text('profile-state','Loading');text('history-status','Loading customer profile…');$('refresh').disabled=true;
 try{
  const customers=await api('/api/v1/customers/search',{mobile:state.mobile});
  if(current!==state.load)return;
  if(!Array.isArray(customers)||!customers.length){text('profile-state','No match');text('history-status','No customer matched this mobile. No ticket can be created.');return;}
  $('customer-options').replaceChildren();
  if(customers.length===1&&customers[0].mobile===state.mobile){await selectCustomer(customers[0]);}
  else{
   text('profile-state','Selection needed');text('profile-note',customers.some(c=>c.mobile!==state.mobile)?'Returned customer mobile differs from the launch mobile. Development integrations may return synthetic data. Select explicitly to continue.':'More than one customer matched. Select the correct customer.');
   for(const customer of customers){const button=document.createElement('button');button.className='secondary wide';button.textContent=`Select ${customer.name} · ${customer.mobile}`;button.onclick=()=>selectCustomer(customer).catch(e=>notice(e.message));$('customer-options').append(button);}
   text('history-status','Select a returned customer to load ticket history.');
  }
 }catch(error){text('profile-state','Unavailable');text('history-status','Customer information could not be loaded.');notice(error.message);}
 finally{$('refresh').disabled=false;}
}
async function selectCustomer(customer){
 state.customer=customer;state.page=1;$('customer-options').replaceChildren();
 text('customer-name',customer.name);text('customer-reference',customer.reference);text('customer-mobile',customer.mobile);text('customer-cif',customer.cif);text('customer-initials',customer.name.split(/\s+/).map(w=>w[0]).slice(0,2).join(''));text('profile-state',customer.reference==='SYNTHETIC-CUSTOMER'?'Synthetic customer':'Customer selected');
 text('profile-note',customer.reference==='SYNTHETIC-CUSTOMER'?'Synthetic development customer. Demographic fields are unavailable in the current API. Tickets created in SQL mode are real records.':'Demographic fields marked Not available are not supplied by the current customer API.');$('create').disabled=false;await Promise.all([loadTickets(),loadRelationships(customer)]);
}
function badge(container,value,type='unknown'){container.replaceChildren();const span=document.createElement('span');span.className='status-badge '+type;span.textContent=value;container.append(span);}
function showDetails(title,records){text('details-title',title);$('details-content').replaceChildren();for(const record of records){const dl=document.createElement('dl');dl.className='details-list';for(const [key,value] of Object.entries(record)){const dt=document.createElement('dt');dt.textContent=key;const dd=document.createElement('dd');dd.textContent=value??'Not available';dl.append(dt,dd);}$('details-content').append(dl);}if(!records.length)text('details-content','No records returned.');$('details-dialog').showModal();}
async function loadRelationships(customer){
 const systems=[
  {id:'core',path:'accounts',title:'Core Banking · Account summaries',project:r=>({'Account':r.maskedAccount,'Product':r.product,'Currency':r.currency}),identifier:records=>customer.cif||records[0]?.maskedAccount},
  {id:'card',path:'cards',title:'Card System · Card summaries',project:r=>({'Card last four':r.lastFour,'Status':r.status}),identifier:records=>records[0]?.lastFour?`•••• ${records[0].lastFour}`:null},
  {id:'ib',path:'internet-banking',title:'Internet Banking',project:r=>({'User ID':r.maskedUserId,'Status':r.status}),identifier:records=>records[0]?.maskedUserId}
 ];
 await Promise.all(systems.map(async system=>{
  text(system.id+'-status','Loading…');
  try{
   const data=await api('/api/v1/customers/'+system.path,{reference:customer.reference});
   if(customer!==state.customer)return;
   const records=Array.isArray(data)?data:data?[data]:[];
   badge($(system.id+'-status'),records.length?'Available':'None returned',records.length?'good':'unknown');
   text(system.id+'-identifier',system.identifier(records)||'—');
   $(system.id+'-details').disabled=records.length===0;
   $(system.id+'-details').onclick=()=>showDetails(system.title,records.map(system.project));
  }catch(error){if(customer!==state.customer)return;badge($(system.id+'-status'),'Unavailable');text(system.id+'-identifier','Integration unavailable');$(system.id+'-details').disabled=true;$(system.id+'-status').title=error.message;}
 }));
}
async function loadTickets(){
 if(!state.customer)return;
 const customer=state.customer,page=state.page,pageSize=state.historySize;
 $('ticket-rows').replaceChildren();$('previous').disabled=true;$('next').disabled=true;text('history-status','Loading recent tickets…');
 try{
  const records=rows(await api('/api/v1/tickets/ticket/get-recent-by-customer',{customerMobile:customer.mobile,page,pageSize}));
  if(customer!==state.customer||page!==state.page||pageSize!==state.historySize)return;
  text('ticket-count',records.length);text('page-label',`Page ${state.page}`);text('history-status',records.length?'Recent requests for the selected customer.':'No tickets on this page.');
  for(const [index,record] of records.entries()){
   const tr=document.createElement('tr');
   const values=[(page-1)*pageSize+index+1,record.ticketNumber||record.requestID,record.requestTitle,'Not provided',record.createdAt?new Date(record.createdAt).toLocaleString():null,null,'Not provided'];
   for(const value of values){const td=document.createElement('td');td.textContent=value??'—';tr.append(td);}
   const status=record.approvalStatus==='PENDING'?'Pending Approval':record.approvalStatus==='REJECTED'?'Rejected':record.approvalStatus==='APPROVED'?'Approved · status unavailable':record.approvalStatus||'Not available';
   badge(tr.children[5],status,record.approvalStatus==='PENDING'?'pending':record.approvalStatus==='REJECTED'?'rejected':'unknown');
   const td=document.createElement('td');const button=document.createElement('button');button.className='quiet';button.textContent='View';button.onclick=()=>viewTicket(record.requestID);td.append(button);tr.append(td);$('ticket-rows').append(tr);
  }
  $('previous').disabled=state.page<=1;$('next').disabled=records.length<pageSize;
 }catch(error){text('history-status','Ticket history is unavailable.');notice(error.message);}
}
async function viewTicket(requestID){
 try{const ticket=rows(await api('/api/v1/tickets/ticket/get-by-id',{requestID}))[0];if(!ticket)throw Error('Ticket not found or access unavailable.');showDetails('Service Request', [{'Request ID':ticket.requestID,'Ticket number':ticket.ticketNumber,'Request title':ticket.requestTitle,'Approval status':ticket.approvalStatus,'Current status ID':ticket.currentStatusID,'Created':ticket.createdAt?new Date(ticket.createdAt).toLocaleString():null}]);}catch(error){notice(error.message);}
}
async function openTicket(){
 if(!state.customer)return;
 $('ticket-form').reset();state.payload=null;state.key=crypto.randomUUID();state.version=null;state.fields=[];$('description').disabled=false;$('submit-status').hidden=true;$('submit-ticket').disabled=true;$('dynamic-fields').replaceChildren();$('service').replaceChildren(new Option('Select a service',''));text('ticket-customer',`${state.customer.name} · ${state.customer.mobile}`);text('version-status','Loading available services…');$('ticket-dialog').showModal();
 try{
  let services=[];let page=1;let batch;
  do{batch=rows(await api('/api/v1/configuration/service/list',{page,pageSize:200}));services.push(...batch);page++;}while(batch.length===200&&page<=50);
  state.services=services.filter(s=>s.isActive===true);for(const service of state.services)$('service').add(new Option(service.serviceName,String(service.serviceID)));
  text('version-status',state.services.length?'Choose a service to load its published fields.':'No active services are configured.');
 }catch(error){text('version-status',error.message);}
}
let versionRequest=0;
async function loadVersion(){
 const request=++versionRequest;state.version=null;state.fields=[];$('submit-ticket').disabled=true;$('dynamic-fields').replaceChildren();if(!$('service').value)return;
 text('version-status','Loading published configuration…');
 try{
  const version=rows(await api('/api/v1/configuration/service-version/get-published',{serviceID:Number($('service').value)}))[0];
  if(request!==versionRequest)return;
  if(!version||version.versionStatus!=='PUBLISHED')throw Error('This service has no published version.');
  const fields=rows(await api('/api/v1/configuration/service-field/get-by-version',{versionID:version.versionID})).filter(f=>f.isActive);
  if(request!==versionRequest)return;
  state.fields=fields;
  for(const field of fields){
   if(field.fieldType==='DROPDOWN')throw Error('Dropdown-backed services are not supported in this first UI increment. Choose another service.');
   if(!['TEXT','TEXTAREA'].includes(field.fieldType))throw Error('This service contains an unsupported field type.');
   if(field.isVisible===false||field.isReadOnly)continue;
   const label=document.createElement('label');label.textContent=field.fieldLabel+(field.isRequired?' *':' (optional)');const control=document.createElement(field.fieldType==='TEXTAREA'?'textarea':'input');control.id=`field-${field.fieldID}`;control.required=!!field.isRequired;control.value=field.defaultValue??'';control.placeholder=field.placeholder??'';if(field.minLength!=null)control.minLength=field.minLength;if(field.maxLength!=null)control.maxLength=field.maxLength;label.append(control);if(field.helpText){const help=document.createElement('span');help.className='fine';help.textContent=field.helpText;label.append(help);}$('dynamic-fields').append(label);
  }
  state.version=version;state.key=crypto.randomUUID();text('version-status',`Published version ${version.versionNumber} · ${fields.length} active field(s)`);$('submit-ticket').disabled=false;
 }catch(error){text('version-status',error.message);}
}
$('launch-form').addEventListener('submit',async event=>{event.preventDefault();const mobile=$('mobile-input').value.trim();if(!validateLaunch(mobile,state.agentId)){notice('Enter a valid mobile number.');return;}state.mobile=mobile;state.incoming||=mobile;await verify();});
$('verify').onclick=verify;
$('dev-login').onclick=async()=>{try{if(state.agentId&&state.agentId!=='AGENT001')throw Error('The development session is AGENT001. It cannot authenticate another launch Agent ID.');await csrf();await api('/api/v1/auth/development/session',{persona:'Agent'});await verify();}catch(error){notice(error.message);}};
$('logout').onclick=async()=>{try{await csrf();await api('/api/v1/auth/logout',{});state.session=null;clearCustomer();$('workspace').hidden=true;$('session-panel').hidden=false;text('agent-name','Session required');text('agent-id','Signed out');$('logout').hidden=true;}catch(error){notice(error.message);}};
$('refresh').onclick=()=>loadCustomers();$('previous').onclick=()=>{state.page--;loadTickets();};$('next').onclick=()=>{state.page++;loadTickets();};$('create').onclick=openTicket;$('service').onchange=loadVersion;
$('clear-mobile').onclick=()=>{$('mobile-input').value='';$('mobile-input').focus();};
$('all-tickets').onclick=()=>{state.historySize=state.historySize===5?20:5;state.page=1;text('history-heading',state.historySize===5?'Last 5 Service Requests':'Service Requests');text('all-tickets',state.historySize===5?'View All Requests ›':'Show Last 5');loadTickets();};
$('close-details').onclick=()=>$('details-dialog').close();
$('close-dialog').onclick=()=>{if(!state.busy){versionRequest++;$('ticket-dialog').close();}};
$('ticket-dialog').addEventListener('cancel',event=>{if(state.busy)event.preventDefault();});
$('ticket-dialog').addEventListener('close',()=>{versionRequest++;});
$('ticket-form').addEventListener('submit',async event=>{
 event.preventDefault();if(state.busy||!state.version||!state.customer)return;
 if(!confirm('Save this ticket to the configured backend and queue its notification?'))return;
 state.busy=true;$('submit-ticket').disabled=true;$('close-dialog').disabled=true;$('service').disabled=true;
 try{
  await csrf();
  const fields=state.fields.filter(f=>f.isVisible!==false&&!f.isReadOnly).map(f=>({fieldID:f.fieldID,value:$(`field-${f.fieldID}`).value})).filter(f=>f.value!=='');
  state.payload??={serviceID:state.version.serviceID,versionID:state.version.versionID,idempotencyKey:state.key,customerReference:state.customer.reference,fields,description:$('description').value};
  const result=await api('/api/v1/tickets',state.payload);
  $('ticket-dialog').close();notice(`Ticket ${result.ticketNumber} created. Request #${result.requestID}.`,true);state.key=null;await loadTickets();
 }catch(error){text('submit-status',error.message+' If the result is uncertain, close this form and check history before creating another ticket. Retrying here sends the identical request and key.');$('submit-status').hidden=false;}
 finally{state.busy=false;$('submit-ticket').disabled=!state.version;$('close-dialog').disabled=false;$('service').disabled=!!state.payload;$('description').disabled=!!state.payload;for(const input of $('dynamic-fields').querySelectorAll('input,textarea'))input.disabled=!!state.payload;}
});
(async()=>{
 const query=new URLSearchParams(location.search);const mobile=query.get('mobile')??'01712345678',agentId=query.get('agentId')??'';
 if(location.search)history.replaceState(null,'',location.pathname); // Avoid persisting launch PII in subsequent navigation/referrers.
 $('mobile-input').value=mobile;text('launch-mobile',mobile);
 try{const health=await api('/health');text('connection',health.mockDatabase?'Mock database · workers off':'SQL mode · workers off');if(health.workersEnabled!==false)throw Error('This local Agent UI requires workers to remain disabled.');}catch(error){notice(error.message);text('connection','API unavailable');return;}
 if(!validateLaunch(mobile,agentId)){notice('Enter a valid mobile number. Agent identity is supplied by the verified session.');return;}
 state.mobile=mobile;state.incoming=mobile;state.agentId=agentId;text('launch-mobile',mobile);await verify();
})();
