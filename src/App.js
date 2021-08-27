const nombre=document.querySelector("#nombre-input");
const edad=document.querySelector("#edad-input");
const sexo=document.querySelector("#sexo-input");
const form=document.querySelector("#saludador-form");

var hoy = new Date();
var hora = hoy.getHours();

var sms_hola="";
var sms_sexo="";
var sms_hora="";
var sms_idioma="";


form.addEventListener("submit", (event) => {

    if(hora < 12){
        sms_hola="Buenos dias "
    }else{
        if(hora < 20){
            sms_hola="Buenas tardes "
        }else{
            sms_hola="Buenas noches "
        }
    }

    if(sexo.value == 'Hombre'){
        sms_sexo="Señor ";
    }else{
        sms_sexo="Señora ";
    }

    if(edad.value < 18){
        sms_sexo="Joven ";
    }

    alert(sms_hola + sms_sexo + nombre.value);
});