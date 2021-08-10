function goToBooks(autorId){
    window.location.href = `libros.html?autorId=${autorId}`;
}
function goToEditAutor(autorId, Nombre, Nacionalidad, FechaNaci, Biografia){
    window.location.href = `Editar.html?autorId=${autorId}&nombre=${Nombre}&nacionalidad=${Nacionalidad}&fechaNaci=${FechaNaci}&biografia=${Biografia}`;
}




window.addEventListener('DOMContentLoaded', function(event){

    let autores = [];
    const baseUrl = 'http://localhost:3030/api';

    function DeleteAutor(event){
        debugger;
        let autorId = this.dataset.deleteAutorId;
        let url = `${baseUrl}/autores/${autorId}`;
        fetch(url, { 
        method: 'DELETE' 
        }).then((data)=>{
            if(data.status === 200){
                alert('deleted');
            }
        }); 
    }

    async function fetchAutores()
    {
        const url = `${baseUrl}/autores`;
        const response = await fetch(url, {
            headers: { 
                "Content-Type": "application/json; charset=utf-8",
                "Authorization": `Bearer ${sessionStorage.getItem("jwt")}`  
            },
            method: 'GET'
        });
        try{
            if(response.status == 200){
                let data = await response.json();
                let autoresLi = data.map( autor => { return `<div class="contenedor"> 
                    <a class="mostrar-Titulo">Nombre:</a><br/>
                    <a class="mostrar-Dato">${autor.nombre} </a><br/>
                    <a class="mostrar-Titulo"> Fecha de Nacimiento:</a><br/>
                    <a class="mostrar-Dato"> ${autor.fechaNacimiento}</a><br/>
                    <a class="mostrar-Titulo"> Nacionalidad:</a><br/>
                    <a class="mostrar-Dato"> ${autor.nacionalidad}</a><br/>
                    <button class="autor-boton" type="button" data-edit-autor-id="${autor.id}" data-edit-autor-nombre="${autor.nombre}" data-edit-autor-fechanacimiento="${autor.FechaNacimiento}" data-edit-autor-nacionalidad="${autor.nacionalidad}" data-edit-autor-biografia="${autor.biografia}">EDITAR</button><br/>
                    <button class="autor-boton" type="button" data-delete-autor-id="${autor.id}">DELETE</button><br/>
                    <button class="autor-boton" type="button" onclick="goToBooks(${autor.id})" target="_self"  data-libros-autor-id="${autor.id}">LIBROS</button><br/>
                </div>`});
                var autorContent = autoresLi.join('');
                document.getElementById('autores-container').innerHTML = autorContent;

                let buttons = document.querySelectorAll('#autores-container div button[data-delete-autor-id]');
                for (const button of buttons) {
                    button.addEventListener('click', DeleteAutor);
                }

                let buttons1 = document.querySelectorAll('#autores-container div button[data-libros-autor-id]');
                for (const button of buttons1) {
                    button.addEventListener('click', GetAutor);
                }

                let buttons2 = document.querySelectorAll('#autores-container div button[data-edit-autor-id]');
                for (const button of buttons2) {
                    button.addEventListener('click', EditAutor);
                }
                
            } else {
                var errorText = await response.text();
                alert(errorText);
            }
        } catch(error){
            var errorText = await error.text();
            alert(errorText);
        }
    }

    function GetAutor(event){
        debugger;
        let autorId = this.dataset.deleteAutorId;
        let url = `${baseUrl}/autores/${autorId}`;
        fetch(url, { 
        method: 'GET' 
        }).then((data)=>{
            if(data.status === 200){
                alert('deleted');
            }
        });

    }

    
    function EditAutor(event){
    
        let autorId = this.dataset.editAutorId;
    
        let Nombre = this.dataset.editAutorNombre;
    
        let Nacionalidad = this.dataset.editAutorNacionalidad;
    
        let FechaNaci = this.dataset.editAutorFechanacimiento;
    
        let Biografia = this.dataset.editAutorBiografia;

        let Imagen = "urlImagenAutor";
    
        goToEditAutor(autorId, Nombre, Nacionalidad, FechaNaci, Biografia, Imagen);

    }

    
    async function fetchDataAsync() {
        try {
            debugger;
            const response = await fetch('http://localhost:3030/api/autores', {
                headers: { 
                    "Content-Type": "application/json; charset=utf-8",
                    "Authorization": `Bearer ${sessionStorage.getItem("jwt")}`  
                },
                method: 'GET'
            })
            
            const json = await response.json();
            document.querySelector('body .container').innerHTML = '<ul>' + json.map(function (autor) {
                return '<li>' + autor.name + ' - ' + autor.name +  `<button type="button" onclick="goToBooks(${autor.id})">View</button> ` + '</li>';
            }).join('') + '</ul>'
        } catch (error) {
            console.log(error);
        }
    }
   
    function PostAutor(event)
    {
        debugger;
        event.preventDefault();
        let url = `${baseUrl}/autores`;
        
        if(!event.currentTarget.nacionalidad.value)
        {
            event.currentTarget.nacionalidad.style.backgroundColor = 'red';
            return;
        }

        var data = {
            Nombre: event.currentTarget.nombre.value,
            Nacionalidad: event.currentTarget.nacionalidad.value,
            FechaNacimiento: event.currentTarget.fechaNacimiento.value,
            Biografia: event.currentTarget.biografia.value
        };

        fetch(url, {
            headers: { "Content-Type": "application/json; charset=utf-8" },
            method: 'POST',
            body: JSON.stringify(data)
        }).then(response => {
            if(response.status === 201){
                alert('autor was created');
            } else {
                response.text()
                .then((error)=>{
                    alert(error);
                });
            }
        });
        
    }    
    fetchAutores();
    document.getElementById('fetch-btn').addEventListener('click', fetchAutores);
    document.getElementById('create-autor-frm').addEventListener('submit', PostAutor);
});

//https://www.freecodecamp.org/news/a-practical-es6-guide-on-how-to-perform-http-requests-using-the-fetch-api-594c3d91a547/