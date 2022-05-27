window.onload = function(){
    getData();
};

function getData (){

    fetch('https://localhost:7090/api/usuarios')
    .then(response => response.json())
    .then(data => {

        //console.log(data)

        let grdDatos = document.getElementById('grdDatos');

        data.forEach(element => {
            grdDatos.insertRow().innerHTML = `
            <td>${element.Id}</td>
            <td>${element.Nombre}</td>
            <td>${element.Email}</td>
            <td>${element.Dni}</td>
            <td class="float-end">
                <button class="btn btn-dark">Actualizar</button>
                <button class="btn btn-danger">Eliminar</button>
            </td>
        `;

        });
    });
}
