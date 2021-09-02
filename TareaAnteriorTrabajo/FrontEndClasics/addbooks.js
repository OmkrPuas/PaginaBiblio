




window.addEventListener('DOMContentLoaded', function(event){

    let autores = [];
    const baseUrl = 'http://localhost:3030/api';

    var queryParams = window.location.search.split('?');
    var autorId= queryParams[1].split('=')[1];

    document.getElementById("autorId").textContent= autorId;

    function fetchAutores()
    {
        debugger;
        const url = `${baseUrl}/autores`;
        let status;
        fetch(url)
        .then((response) => { 
            status = response.status;
            return response.json();
        })
        .then((data) => {
            if(status == 200)
            {
                console.log(data)
                let autoresLi = data.map( autor => { return `<li> Title: ${autor.title} | Composer: ${autor.composer} | Form: ${autor.form} </li>`});
                var autorContent = `<ul>${autoresLi.join('')}</ul>`;
                document.getElementById('autores-container').innerHTML = autorContent;
            } else {
                alert(data);
            }
        });
    }

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

    async function fetchLibros()
    {
        const url = `${baseUrl}/autores/1/libros`;
        let response = await fetch(url);
        try{
            if(response.status == 200){
                let data = await response.json();
                let librosLi = data.map( libro => { 
                
                    const imageUrl = team.imagePath? `${baseRawUrl}/${team.imagePath}` : "";
                    return `<div class="contenedor" style="background-image:url('${imageUrl}');background-position: center;background-repeat: no-repeat;background-size: 100%;"> 
                    <button type="button" style="height: 20px;" data-get-libro-id="${libro.id}">VER</button><br/>
                    </div>
                `});
                var libroContent = librosLi.join('');
                document.getElementById('libros-container').innerHTML = libroContent;

                let buttons = document.querySelectorAll('#libros-container div button[data-get-libro-id]');
                for (const button of buttons) {
                    button.addEventListener('click', fetchLibro);
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
   
    function PostFormLibro(event)
    {
        
        debugger;
        event.preventDefault();
        let url = `${baseUrl}/autores/${autorId}/libros`;
        puntuacion = 0;
        
        const formData = new FormData();
        formData.append('Titulo', event.currentTarget.titulo.value);
        formData.append('AutorId', autorId);
        formData.append('Genero',event.currentTarget.genero.value);
        formData.append('AnioPublicacion', event.currentTarget.anioPublicacion.value);
        formData.append('Puntuacion', puntuacion);
        formData.append('Image', event.currentTarget.Image.files[0]);
        debugger;

        fetch(url, {
            method: 'POST',
            body: formData
        }).then(response => {
            if(response.status === 201){
                alert('book was created');
            } else {
                response.text()
                .then((error)=>{
                    alert("error: "+error);
                    alert("error2: "+response.status);
                });
            }
        });
    }   
    document.getElementById('fetch-btn').addEventListener('click', fetchAutores);
    document.getElementById('create-libro-form-frm').addEventListener('submit', PostFormLibro)
});
