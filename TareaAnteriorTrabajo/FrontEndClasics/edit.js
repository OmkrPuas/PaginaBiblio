




window.addEventListener('DOMContentLoaded', function(event){

    const querystring = window.location.search;
    const params = new URLSearchParams(querystring);

    autorId = params.get('autorId');
    Nombre = params.get('nombre');
    Nacionalidad = params.get('nacionalidad');
    FechaNacimiento = params.get('fechaNaci');
    Biografia = params.get('biografia');

    document.getElementById("edit-autorId").value= autorId;
    document.getElementById("edit-nombre").value= Nombre;
    document.getElementById("edit-nacionalidad").value= Nacionalidad;
    document.getElementById("edit-fechaNacimiento").value= FechaNacimiento;
    document.getElementById("edit-biografia").value= Biografia;

    let autores = [];
    const baseUrl = 'http://localhost:3030/api';


    function EditAutor(event){
        debugger;
        event.preventDefault();
        var idAutor = event.currentTarget.idAutor.value;
        let url = `${baseUrl}/autores/${idAutor}`;

        var data = {
            autorId : event.currentTarget.idAutor.value,
            Nombre: event.currentTarget.nombre.value,
            Nacionalidad: event.currentTarget.nacionalidad.value,
            FechaNacimiento: event.currentTarget.fechaNacimiento.value,
            Biografia: event.currentTarget.biografia.value
        };

        fetch(url, {

            headers: { 
                "Content-Type": "application/json; charset=utf-8" ,
                "Authorization": `Bearer ${sessionStorage.getItem("jwt")}`
            },
            method: 'PUT',
            body: JSON.stringify(data)
        }).then(response => {
            if(response.status === 200){
                alert('autor was updated');
            } else {
                response.text()
                .then((error)=>{
                    alert(error);
                });
            }
        });
    }
    document.getElementById('edit-autor-frm').addEventListener('submit', EditAutor);
});

//https://www.freecodecamp.org/news/a-practical-es6-guide-on-how-to-perform-http-requests-using-the-fetch-api-594c3d91a547/