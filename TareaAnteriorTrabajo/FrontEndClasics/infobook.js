function goToAddBooks(autorId){
    window.location.href = `AgregarLibro.html?autorId=${autorId}`;
}
function goToEditBooks(autorId){
    window.location.href = `EditarLibro.html?autorId=${autorId}`;
}



window.addEventListener('DOMContentLoaded', function(event){

    const querystring = window.location.search;
    const params = new URLSearchParams(querystring);

    autorId = params.get('autorId');
    libroId = params.get('libroId');
    titulo = params.get('titulo');
    genero = params.get('genero');
    anioPublicacion = params.get('year');
    imagen = params.get('imagen');


    document.getElementById("autorId").textContent= autorId;
    document.getElementById("libroId").textContent= libroId;
    document.getElementById("titulo").textContent= titulo;
    document.getElementById("genero").textContent= genero;
    document.getElementById("anioPublicacion").textContent= anioPublicacion;
    document.getElementById("imagen").textContent= imagen;

    let autores = [];
    let libros= [];
    const baseRawUrl = 'http://localhost:3030';
    const baseUrl = `${baseRawUrl}/api`;


    function DeleteLibro(event){
        debugger;
        let libroId = this.dataset.deletelibroId;
        let url = `${baseUrl}/autores/${autorId}/libros/${libroId}`;
        fetch(url, { 
        method: 'DELETE' 
        }).then((data)=>{
            if(data.status === 200){
                alert('deleted');
            }
        }); 
    }

    async function fetchAutor(){

        const url = `${baseUrl}/autores/${autorId}`;
        fetch(url, {
            headers: { 
                "Content-Type": "application/json; charset=utf-8",
                "Authorization": `Bearer ${sessionStorage.getItem("jwt")}`  
            },
            method: 'GET'
        })
        .then(response => response.json() )
        .then(autor => {
            let element = document.getElementById('autor-info-container')
            element.innerHTML = `
            <div class="contenedor-libro">
            <a class="mostrar-Titulo">Nombre del Autor</a>
            <a class="mostrar-Dato">${autor.nombre}</a><br/>
            <a class="mostrar-Titulo">Fecha de Nacimiento</a>
            <a class="mostrar-Dato">${autor.fechaNacimiento}</a><br/>
            <a class="mostrar-Titulo">Nacionalidad</a>
            <a class="mostrar-Dato">${autor.nacionalidad}</a><br/>
            <iframe style="width: 700px;height: 500px;" src="${autor.biografia}"></iframe>
            </div>`;
            console.log(libro)
        })
        .catch(err=>console.log(err));

    }
    async function fetchLibro()
    {

        const url = `${baseUrl}/autores/${autorId}/libros/${libroId}`;
        fetch(url)
        .then(response => response.json() )
        .then(libro => {
            const imageUrl = libro.imagePath? `${baseRawUrl}/${libro.imagePath}` : "";
            let element = document.getElementById('libros-info-container')
            element.innerHTML = `
            <div class="contenedor-autor">
            <img src="${imageUrl}" alt="Avatar" class="roundImage-info" style="padding: 5px;width: 300px;height: 500px;"> 
            <a class="mostrar-Titulo">Titulo</a>
            <a class="mostrar-Dato">${titulo}</a><br/>
            <a class="mostrar-Titulo">Genero</a>
            <a class="mostrar-Dato">${genero}</a><br/>
            <a class="mostrar-Titulo">Anio de Publicacion</a>
            <a class="mostrar-Dato">${anioPublicacion}</a><br/>
            </div>`;
            console.log(libro)
        })
        .catch(err=>console.log(err))
    }
   
    function PostFormLibro(event)
    {
        goToAddBooks(autorId);
    }

    function EditFormLibro(event)
    {
        goToEditBooks(autorId);
    }

    function Inicial(event)
    {
        fetchLibro();
        fetchAutor();
    }
    Inicial();
    document.getElementById('toAddBook').addEventListener('click', PostFormLibro);
    document.getElementById('toEditBook').addEventListener('click', EditFormLibro);
});

//https://www.freecodecamp.org/news/a-practical-es6-guide-on-how-to-perform-http-requests-using-the-fetch-api-594c3d91a547/