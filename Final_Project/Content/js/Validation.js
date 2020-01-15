function validateForm(event){
//gather all teh HTML form controls into a collection

let controls=document.getElementsByClassName('form-control');
console.log(controls);

let validationMessages=document.getElementsByClassName('invalid');
console.log(validationMessages)

//regualer expression object for email
//the args passed to RegExp must begin and end with a /
let rxpEmail=new RegExp(/^(\D)+(\w)*((\.(\w)+)?)+@(\D)+(\w)*((\.(\D)+(\w)*)+)?(\.)[a-z]{2,}$/)
//regex name
let rxpName=new RegExp(/^[a-zA-Z]*$/)
//check they length of all controls - to see if they have been filled out

if(controls['name'].value.length==0 || controls['email'].value.length==0||controls['subject'].value.length==0||controls['message'].value.length==0) {

//stop the form from submitting
event.preventDefault();

//check the individual form field to see if ehty were the one that was empty
/******************NAME VALIDATORS***********************/
//LENGTH
        if(controls['name'].value.length==0){
            validationMessages['rfvName'].textContent="*** Name is required ***";
        }
        else {
            validationMessages['rfvName'].textContent="";
        }
/******************EMAIL VALIDATORS***********************/
//LENGTH
        if(controls['email'].value.length==0){
            validationMessages['rfvEmail'].textContent="*** Email is required ***";
        }
        else {
            validationMessages['rfvEmail'].textContent="";
        }

/******************SUBJECT VALIDATORS***********************/
//LENGTH
        if(controls['subject'].value.length==0){
            validationMessages['rfvSubject'].textContent="*** Subject is required ***";
        }
        else {
            validationMessages['rfvSubject'].textContent="";
        }
/******************MESSAGE VALIDATORS***********************/
//LENGTH
        if(controls['message'].value.length==0){
            validationMessages['rfvMessage'].textContent="*** Message is required ***";
        }
        else {
            validationMessages['rfvMessage'].textContent="";
        }
    }
//if everything is filled out then we wont hit that if
//regex email
if(!rxpEmail.test(controls['email'].value)&& controls['email'].value.length>0){
event.preventDefault();
    validationMessages['rfvEmail'].textContent="***Please enter a valid Email***"
}
if(rxpEmail.test(controls['email'].value)&& controls['email'].value.length>0){
    validationMessages['rfvEmail'].textContent="";
}

if(!rxpName.test(controls['name'].value)&& controls['name'].value.length>0){
event.preventDefault();
    validationMessages['rfvName'].textContent="***Please enter a valid Name***"
}
if(rxpEmail.test(controls['name'].value)&& controls['name'].value.length>0){
    validationMessages['rfvName'].textContent="";
}


