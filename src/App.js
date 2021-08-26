const nombre=document.querySelector("#nombre-input");
const sexo=document.querySelector("#sexo-input");
const form=document.querySelector("#saludador-form");

form.addEventListener("submit", (event) => {
    if(sexo.value == 'Hombre'){
        alert("Hola señor " + nombre.value);
    }else{
        alert("Hola señora " + nombre.value);
    }
});