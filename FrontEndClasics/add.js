




window.addEventListener('DOMContentLoaded', function(event){

    let autores = [];
    const baseUrl = 'http://localhost:3030/api';

   
    function PostAutor(event)
    {
        debugger;
        event.preventDefault();
        let url = `${baseUrl}/autores`;
        
        const formDataAutor = new FormData();
        formDataAutor.append('Nombre', event.currentTarget.nombre.value);
        formDataAutor.append('Nacionalidad', event.currentTarget.nacionalidad.value);
        formDataAutor.append('FechaNacimiento',event.currentTarget.fechaNacimiento.value);
        formDataAutor.append('Biografia', event.currentTarget.biografia.value);
        formDataAutor.append('Imagen', event.currentTarget.Imagen.files[0]);
        debugger;

        fetch(url, {
            headers: { 
                "Authorization": `Bearer ${sessionStorage.getItem("jwt")}`  
            },
            method: 'POST',
            body: formDataAutor
        }).then(response => {
            if(response.status === 201){
                alert('autor was created');
            } else {
                response.text()
                .then((error)=>{
                    alert("error: "+error);
                    alert("error2: "+response.status);
                });
            }
        });
        
    }    
    document.getElementById('create-autor-frm').addEventListener('submit', PostAutor);
});
