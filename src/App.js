const nombre=document.querySelector("#nombre-input");
const edad=document.querySelector("#edad-input");
const sexo=document.querySelector("#sexo-input");
const idioma=document.querySelector("#idioma-input");
const form=document.querySelector("#saludador-form");

var hoy = new Date();
var hora = hoy.getHours();

var sms_hola="";
var sms_sexo="";
var sms_hora="";
var sms_idioma="";


form.addEventListener("submit", (event) => {

    if(idioma.value == "Spanish"){
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
    }else{
        if(idioma.value == "English"){
            if(hora < 12){
                sms_hola="Good morning "
            }else{
                if(hora < 20){
                    sms_hola="Good afternoon "
                }else{
                    sms_hola="Good night "
                }
            }
        
            if(sexo.value == 'Hombre'){
                sms_sexo="Mister ";
            }else{
                sms_sexo="Ms ";
            }
        
            if(edad.value < 18){
                sms_sexo="Dude ";
            }
        }else{
            
        }
    }

    

    alert(sms_hola + sms_sexo + nombre.value);
});