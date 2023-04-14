const Alumnos = {
    xs: 12, sm:12, md: 6, lg:3,
    background: "purple",
    hover: true,
    goto: '',
    header: {
        title: "Alumnos",
    },
    body: {
        title: "Ultimos Alumnos",
        table: {
            small: true, 
            titulos: [ {titulo: "Nombre"}, {titulo: "Creado en"} ], 
            filas: [
                    {datos: [{dato: "John"}, {dato: "12/12/2017"}]},
                    {datos: [{dato: "Mery"}, {dato: "02/02/2017"}]},
                    {datos: [{dato: "Pepe"}, {dato: "01/10/2017"}]},
                    {datos: [{dato: "Laura"}, {dato: "31/01/2017"}]}
            ]
        }
    }

}

const Profesores = {
    xs: 12, sm:12, md: 6, lg:3,
    background: "purple",
    hover: true,
    goto: '/administrador/list',
    header: {
        title: "Profesores",
    },
    body: {
        title: "Ultimos Profesores",
        table: {
            small: true, 
            titulos: [ {titulo: "Nombre"},  {titulo: "Creado en"} ], 
            filas: [
                    {datos: [{dato: "John"}, {dato: "12/12/2017"}]},
                    {datos: [{dato: "Mery"}, {dato: "02/02/2017"}]},
                    {datos: [{dato: "Pepe"}, {dato: "27/04/2017"}]},
                    {datos: [{dato: "Max"}, {dato: "01/10/2017"}]},
                    {datos: [{dato: "Laura"}, {dato: "31/01/2017"}]}
            ]
        }
    }
}

const Materias = {
    xs: 12, sm:12, md: 6, lg:3,
    background: "orange",
    hover: true,
    goto: '/administrador/list',
    header: {
        title: "Materias",
    },
    body: {
        title: "Ultimas Materias",
        table: {
            small: true, 
            titulos: [ {titulo: "Nombre"}, {titulo: "Creado en"} ], 
            filas: [
                    {datos: [{dato: "Fisica"}, {dato: "12/12/2017"}]},
                    {datos: [{dato: "Matematica"}, {dato: "02/02/2017"}]},
                    {datos: [{dato: "Estadistica"}, {dato: "27/04/2017"}]},
                    {datos: [{dato: "Geofrafia"}, {dato: "01/10/2017"}]},
                    {datos: [{dato: "Ingenieria del software"}, {dato: "31/01/2017"}]}
            ]
        }
    }
}

const Clases = {
    xs: 12, sm:12, md: 6, lg:3,
    background: "blue",
    hover: true,
    goto: '/administrador/list',
    header: {
        title: "Clases",
    },
    body: {
        title: "Ultimas Clases",
        table: {
            small: true, 
            titulos: [ {titulo: "Nombre"},  {titulo: "Creado en"} ], 
            filas: [
                    {datos: [{dato: "Primero A"},{dato: "12/12/2017"}]},
                    {datos: [{dato: "Primero B"}, {dato: "02/02/2017"}]},
                    {datos: [{dato: "Segundo A"},{dato: "27/04/2017"}]},
                    {datos: [{dato: "Segundo B"},  {dato: "01/10/2017"}]},
                    {datos: [{dato: "Tercero"},  {dato: "31/01/2017"}]}
            ]
        }
    }
}

const Stats = {
    xs: 12, sm:12, md: 12, lg:12,
    background: "gray",
    goto: '/',
    header: {
        title: "Promedio de Calificaciones",
    },
    body: {
        title: "Promedio de Calificaciones",
        graph: {
            
        }
    }
}


export {Alumnos, Profesores, Materias, Clases, Stats};