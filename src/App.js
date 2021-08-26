const nombre=document.querySelector("#nombre-input");
const edad=document.querySelector("#edad-input");
const sexo=document.querySelector("#sexo-input");
const form=document.querySelector("#saludador-form");

form.addEventListener("submit", (event) => {
    if(edad.value > 18){
        if(sexo.value == 'Hombre'){
            alert("Hola señor " + nombre.value);
        }else{
            alert("Hola señora " + nombre.value);
        }
    }else{
        if(sexo.value == 'Hombre'){
            alert("Hola joven " + nombre.value);
        }else{
            alert("Hola señorita " + nombre.value);
        }
    }
});