const nombre=document.querySelector("#nombre-input");
const form=document.querySelector("#saludador-form");


form.addEventListener("submit", (event) => {
    alert("Hola " + nombre.value);
});