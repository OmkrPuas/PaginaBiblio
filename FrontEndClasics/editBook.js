function goToAddBooks(autorId){
    window.location.href = `AgregarLibro.html?autorId=${autorId}`;
}
function goToBooks(autorId){
    window.location.href = `libros.html?autorId=${autorId}`;
}


window.addEventListener('DOMContentLoaded', function(event){

    const querystring = window.location.search;
    const params = new URLSearchParams(querystring);

    autorIde = params.get('autorId');
    libroId = params.get('libroId');
    titulo = params.get('titulo');
    genero = params.get('genero');
    anioPublicacion = params.get('year');

    document.getElementById("edit-lid").value = libroId; 
    document.getElementById("edit-titulo").value = titulo; 
    document.getElementById("edit-genero").value = genero; 
    document.getElementById("edit-anioPublicacion").value = anioPublicacion; 

    var queryParams = window.location.search.split('?');
    var autorId= queryParams[1].split('=')[1];

    document.getElementById("autorId").textContent= autorIde;

    let autores = [];
    const baseRawUrl = 'http://localhost:3030';
    const baseUrl = `${baseRawUrl}/api`;


    function DeleteLibro(event){
        debugger;
        let libroId = this.dataset.deletelibroId;
        let url = `${baseUrl}/autores/${autorIde}/libros/${libroId}`;
        fetch(url, { 
        method: 'DELETE' 
        }).then((data)=>{
            if(data.status === 200){
                alert('deleted');
            }
        }); 
    }

    function PostFormLibro(event)
    {
        goToAddBooks(autorId);
    }
    function EditLibro(event){
        debugger;
        event.preventDefault();
        var idlibro = event.currentTarget.libroid.value;
        let url = `${baseUrl}/autores/${autorIde}/libros/${idlibro}/like`;

        const formData = new FormData();
        formData.append('Titulo', event.currentTarget.titulo.value);
        formData.append('Genero',event.currentTarget.genero.value);
        formData.append('AnioPublicacion', event.currentTarget.anioPublicacion.value);
        debugger;

        var data = {
            Titulo : event.currentTarget.titulo.value,
            Genero : event.currentTarget.genero.value,
            AnioPublicacion : event.currentTarget.anioPublicacion.value
        };

        
        fetch(url, {
            headers: { 
                "Content-Type": "application/json; charset=utf-8",
                "Authorization": `Bearer ${sessionStorage.getItem("jwt")}`  
            },
            method: 'PUT',
            body: JSON.stringify(data)
        }).then(response => {
            if(response.status === 200){
                alert('book was edited');
                goToBooks(autorIde);
            } else {
                response.text()
                .then((error)=>{
                    alert("error: "+error);
                    alert("error2: "+response.status);
                });
            }
        });
    }
    document.getElementById('toAddBook').addEventListener('click', PostFormLibro);
    document.getElementById('edit-libro-frm').addEventListener('submit', EditLibro);
});

//https://www.freecodecamp.org/news/a-practical-es6-guide-on-how-to-perform-http-requests-using-the-fetch-api-594c3d91a547/