//background options: orange, purple, blue, yellow, gray

const ExampleCard1 = {
    background: "orange",
    hover: true,
    goto: '/colegios/list',
    header: {
        title: "Colegios",
    },
    body: {
        title: "Ultimos Colegios",
        table: {
            small: true, 
            titulos: [ {titulo: "Nombre"}, {titulo: "Creado el"} ], 
            filas: [
                    {datos: [{dato: "Ceuce"}, {dato: "12/12/2017"}]},
                    {datos: [{dato: "Cree"},  {dato: "02/02/2017"}]},
                    {datos: [{dato: "Principito"}, {dato: "27/04/2017"}]},
                    {datos: [{dato: "Inmaculada"}, {dato: "01/10/2017"}]},
                    {datos: [{dato: "Morinigo"}, {dato: "31/01/2017"}]}
            ]
        }
    }

}

const ExampleCard2 = {
    background: "orange",
    hover: true,
    goto: '/administrador/list',
    header: {
        title: "Administradores",
    },
    body: {
        title: "Ultimos Administradores",
        table: {
            small: true, 
            titulos: [ {titulo: "Nombre"}, {titulo: "Apellido"}, {titulo: "Creado en"} ], 
            filas: [
                    {datos: [{dato: "John"}, {dato: "Doe"}, {dato: "12/12/2017"}]},
                    {datos: [{dato: "Mery"}, {dato: "Jane"}, {dato: "02/02/2017"}]},
                    {datos: [{dato: "Pepe"}, {dato: "Lepew"}, {dato: "27/04/2017"}]},
                    {datos: [{dato: "Max"}, {dato: "Steele"}, {dato: "01/10/2017"}]},
                    {datos: [{dato: "Laura"}, {dato: "Doe"}, {dato: "31/01/2017"}]}
            ]
        }
    }
}

const ExampleCard3 = {

}



export {ExampleCard1, ExampleCard2, ExampleCard3};