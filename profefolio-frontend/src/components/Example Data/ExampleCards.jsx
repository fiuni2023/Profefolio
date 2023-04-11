//background options: orange, purple, blue, yellow, gray

const ExampleCard1 = {
    background: "orange",
    header: {
        title: "Tarjeta de ejemplo 1",
    },
    body: {
        title: "Ultimos maestros",
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

const ExampleCard2 = {

}

const ExampleCard3 = {

}



export {ExampleCard1, ExampleCard2, ExampleCard3};