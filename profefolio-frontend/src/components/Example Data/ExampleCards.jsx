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

const colegios = [
    {   xs: 12, sm:12, md: 6, lg:3,
        background: "yellow",
        hover: true,
        goto: '/',
        header: {
            title: "CREE",
        },
        body: {
            first: {
                title: "5 clases: ",
                subtitle: "Primero A, Primero B, Segundo A..." 
            },
            schedule: {
                main: "Lunes 8AM - Martes 9AM - Miercoles 9AM  - Viernes 7AM - Viernes 9AM",
            }
        }
    },
    
]

const clases = [
    {   xs: 12, sm:12, md: 6, lg:3,
        background: "orange",
        hover: true,
        goto: '/',
        header: {
            title: "Primer Grado",
        },
        body: {
            title: "1er Ciclo 2023",
            first: {
                title: "5 materias: ",
                subtitle: "Matematicas, Ciencias, Quimica..." 
            },
            second: {
                title: "45 alumnos"
            },
            schedule: {
                main: "Viernes 11AM",
                secondary: "2hs" 
            }
        }
    },
]

const materias = [
    {   xs: 12, sm:12, md: 6, lg:3,
        background: "blue",
        hover: true,
        goto: '/',
        header: {
            title: "Ciencias",
        },
        body: {
            first: {
                title: "23 anotaciones ",
            },
            second: {
                title: "4 calificaciones",
            },
            third: {
                title: "1 evento",
            },
            schedule: {
                main: "Viernes 11AM",
                secondary: "2hs" 
            }
        }
    },
]

const infoClase = [
    {   xs: 12, sm:12, md: 6, lg:3,
        background: "purple",
        hover: true,
        goto: '/',
        header: {
            title: "Calificaciones",
        },
        body: {
            first: {
                title: "4 planillas calificadas ",
            },
            second: {
                title: "2 planillas pendientes",
            }
        }
    },
]

const data1 = {
    labels : ["1er Grado", "2do Grado", "3er Grado", "4to Grado", "5to Grado", "6to Grado", "7mo Grado"],
    datasets : [{
        label: "Turno Mañana",
        backgroundColor: "#31BA8D",
        data: [45,50,38,40,35,27,52]
    }, {
        label: "Turno Tarde",
        backgroundColor: "#5181D1",
        data: [35,50,48,30,30,29,32]
    }]
};

const data2 = {
    labels : ["Prueba sumativa 1", "Prueba sumativa 2", "Examen primera etapa", "Prueba sumativa 4" ],
    datasets : [{
        label: "Matematicas",
        backgroundColor: "#FF0000",
        borderColor: "#FF0000",
        data: [8,9.5,6.3,9]
    }]
};

const graficos = [
    {
        xs: 12, sm:12, md: 12, lg:12,
        background: "gray",
        header: {
            title: "Cantidad de alumnos por turno",
        },
        body: {
            graph: {
                id: 'alumnosXturno',
                config: {
                    type: 'bar',
                    data: data1, 
                }
            }
        }
    },
    {
        xs: 12, sm:12, md: 12, lg:12,
        background: "gray",
        header: {
            title: "Promedio de puntajes",
        },
        body: {
            graph: {
                id: 'promedioCalif',
                config: {
                    type: 'line',
                    data: data2, 
                }
            }
        }
    }
]




export {ExampleCard1, ExampleCard2, colegios, clases, materias, infoClase, graficos};