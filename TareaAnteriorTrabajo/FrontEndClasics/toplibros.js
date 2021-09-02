function goToAddBooks(autorId){
    window.location.href = `AgregarLibro.html?autorId=${autorId}`;
}
function goToEditBooks(autorId){
    window.location.href = `EditarLibro.html?autorId=${autorId}`;
}
function goToInfoBook(autorIdu, libroId, titulo, genero, year, imagen){
    window.location.href = `MostrarLibro.html?autorId=${autorIdu}&libroId=${libroId}&titulo=${titulo}&genero=${genero}&year=${year}&imagen=${imagen}`;
}
function goToEditBook(autorId, libroId, titulo, genero, year, imagen){
    window.location.href = `EditarLibro.html?autorId=${autorId}&libroId=${libroId}&titulo=${titulo}&genero=${genero}&year=${year}&imagen=${imagen}`;
}




window.addEventListener('DOMContentLoaded', function(event){

    let autores = [];
    const baseRawUrl = 'http://localhost:3030';
    const baseUrl = `${baseRawUrl}/api`;

    function DeleteLibro(event){
        debugger;
        let libroId = this.dataset.deleteLibroId;
        let autorIda = this.dataset.deleteLibroAutor;
        let url = `${baseUrl}/autores/${autorIda}/libros/${libroId}`;
        fetch(url, { 
        method: 'DELETE' 
        }).then((data)=>{
            if(data.status === 200){
                alert('deleted');
            }
        }); 
    }

    async function fetchLibro()
    {

        let libroId = this.dataset.getLibroId;

        let autorIdu = this.dataset.getLibroAutor;

        let Title = this.dataset.getLibroTitle;

        let Genero = this.dataset.getLibroGenero;

        let Publicacion = this.dataset.getLibroAniopublicacion;

        let Imagen = "urlimagen";

        goToInfoBook(autorIdu, libroId, Title, Genero, Publicacion, Imagen);
    }

    async function EditLibro()
    {
        let libroId = this.dataset.editLibroId;

        let autorIdo = this.dataset.editLibroAutor;

        let Title = this.dataset.editLibroTitle;

        let Genero = this.dataset.editLibroGenero;

        let Publicacion = this.dataset.editLibroAniopublicacion;

        let Imagen = "urlimagen";

        goToEditBook(autorIdo, libroId, Title, Genero, Publicacion, Imagen);
    }

    function AddLike(event){

        debugger;
        let libroId = this.dataset.likeLibroId;
        let autorIde = this.dataset.likeLibroAutor;

        let url = `${baseUrl}/autores/${autorIde}/libros/${libroId}/like`;


        let Puntuacion = this.dataset.likeLibroPuntuacion;
        let Puntuacione = parseInt(Puntuacion) + 1;


        var data = {
            puntuacion:Puntuacione
        };
        debugger;

        fetch(url, {
            headers: { 
                "Content-Type": "application/json; charset=utf-8",
                "Authorization": `Bearer ${sessionStorage.getItem("jwt")}`  
            },
            method: 'PUT',
            body: JSON.stringify(data)
        }).then(response => {
            if(response.status === 200){
                alert('book was liked');
                fetchLibros();
            } else {
                response.text()
                .then((error)=>{
                    alert("error: "+error);
                    alert("error2: "+response.status);
                });
            }
        });
    }

    async function fetchLibros()
    {
        debugger;
        const url = `${baseUrl}/autores/libros/top?orderby=puntuacion`;
        let response = await fetch(url, {
            headers: { 
                "Authorization": `Bearer ${sessionStorage.getItem("jwt")}`  
            }
        });
        try{
            if(response.status == 200){
                let data = await response.json();
                let librosLi = data.map( libro => { 
                
                    const imageUrl = libro.imagePath? `${baseRawUrl}/${libro.imagePath}` : "";
                    return `<div>
                    <img src="${imageUrl}" alt="Avatar" class="roundImage" style="padding: 5px;"> 
                    <button class="libro-boton" type="button" style="height: 20px;" data-edit-libro-id="${libro.id}" data-edit-libro-title="${libro.titulo}" data-edit-libro-genero="${libro.genero}" data-edit-libro-aniopublicacion="${libro.anioPublicacion}" data-edit-libro-autor="${libro.autorId}">EDITAR</button>
                    <button class="libro-boton" type="button" style="height: 20px;" data-get-libro-id="${libro.id}" data-get-libro-title="${libro.titulo}" data-get-libro-genero="${libro.genero}" data-get-libro-aniopublicacion="${libro.anioPublicacion}" data-get-libro-autor="${libro.autorId}">VER</button>
                    <button class="libro-boton" type="button" data-delete-libro-id="${libro.id} data-delete-libro-autor="${libro.autorId}"">DELETE</button><br/>
                    <a class="likes">Likes:</a>
                    <a class="likes">${libro.puntuacion}</a>
                    <button class="like-boton" type="button" data-like-libro-id="${libro.id}" data-like-libro-autor="${libro.autorId}" data-like-libro-puntuacion="${libro.puntuacion}"><img id="like" src="recursos/logoLike.png"></button>
                    <a style="display:none;">${libro.autorId}</a>
                    </div>
                `});
                var libroContent = librosLi.join('');
                document.getElementById('libros-container').innerHTML = libroContent;

                let buttons = document.querySelectorAll('#libros-container div button[data-get-libro-id]');
                for (const button of buttons) {
                    button.addEventListener('click', fetchLibro);
                }

                let buttons1 = document.querySelectorAll('#libros-container div button[data-delete-libro-id]');
                for (const button of buttons1) {
                    button.addEventListener('click', DeleteLibro);
                }

                let buttons2 = document.querySelectorAll('#libros-container div button[data-edit-libro-id]');
                for (const button of buttons2) {
                    button.addEventListener('click', EditLibro);
                }

                let buttons3 = document.querySelectorAll('#libros-container div button[data-like-libro-id]');
                for (const button of buttons3) {
                    button.addEventListener('click', AddLike);
                }
                
            } else {
                var errorText = await response.text();
                alert("errOr: "+errorText);
                alert("errOr2: "+response.status);
            }
        } catch(error){
            var errorText = await error.text();
            alert("error: "+errorText);
            alert("error2: "+response.status);
        }
    }
    fetchLibros();
    document.getElementById('fetch-btn').addEventListener('click', fetchLibros);
    document.getElementById('toAddBook').addEventListener('click', PostFormLibro);
    document.getElementById('toEditBook').addEventListener('click', EditFormLibro);
});

//https://www.freecodecamp.org/news/a-practical-es6-guide-on-how-to-perform-http-requests-using-the-fetch-api-594c3d91a547/