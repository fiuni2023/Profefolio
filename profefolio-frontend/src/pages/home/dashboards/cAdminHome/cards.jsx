const Alumnos = {
    xs: 12, sm:12, md: 6, lg:3,
    background: "yellow",
    hover: true,
    goto: '/alumnos',
    header: {
        title: "Alumnos",
    },
    body: {
        title: "Ultimos Alumnos",
        table: {
            small: true, 
            titulos: [ {titulo: "Nombres"}, {titulo: "Apellidos"}, {titulo: "Grado"} ], 
            filas: [
                    {datos: [{dato: "John"}, {dato: "Doe"}, {dato: "Primero A"}]},
                    {datos: [{dato: "Mery"}, {dato: "Amarilla"}, {dato: "Primero B"}]},
                    {datos: [{dato: "Pepe"}, {dato: "Morinigo"}, {dato: "Primero A"}]},
                    {datos: [{dato: "Laura"}, {dato: "Gancedo"}, {dato: "Segundo TT"}]},
                    {datos: [{dato: "Aldo"}, {dato: "Caldo"}, {dato: "Tercero B"}]}
            ]
        }
    }

}

const Profesores = {
    xs: 12, sm:12, md: 6, lg:3,
    background: "yellow",
    hover: true,
    goto: '/profesor',
    header: {
        title: "Profesores",
    },
    body: {
        title: "Ultimos Profesores",
        table: {
            small: true, 
            titulos: [ {titulo: "Nombre"},  {titulo: "Apellido"} ], 
            filas: [
                    {datos: [{dato: "John"}, {dato: "Amarilla"}]},
                    {datos: [{dato: "Mery"}, {dato: "Cabrera"}]},
                    {datos: [{dato: "Pepe"}, {dato: "Ansaldo"}]},
                    {datos: [{dato: "Max"}, {dato: "Steele"}]},
                    {datos: [{dato: "Laura"}, {dato: "Dern"}]}
            ]
        }
    }
}

const Materias = {
    xs: 12, sm:12, md: 6, lg:3,
    background: "orange",
    hover: true,
    goto: '/materias',
    header: {
        title: "Materias/Ciclos",
    },
    body: {
        title: "Ultimas Materias/Ciclos",
        table: {
            small: true, 
            titulos: [ {titulo: "Nombre"} ], 
            filas: [
                    {datos: [{dato: "Fisica"}]},
                    {datos: [{dato: "Matematica"}]},
                    {datos: [{dato: "Estadistica"}]},
                    {datos: [{dato: "Geofrafia"}]},
                    {datos: [{dato: "Ingenieria del software"}]}
            ]
        },
        table2: {
            small: true, 
            titulos: [ {titulo: "Ciclo"}], 
            filas: [
                    {datos: [{dato: "Nivel Inicial"}]},
                    {datos: [{dato: "Primer Ciclo"}]},
                    {datos: [{dato: "Segundo Ciclo"}]},
                    {datos: [{dato: "Tercer Ciclo"}]},
                    {datos: [{dato: "EEB"}]}
            ]
        }
    }
}

const Clases = {
    xs: 12, sm:12, md: 6, lg:3,
    background: "purple",
    hover: true,
    goto: '/clases',
    header: {
        title: "Clases",
    },
    body: {
        title: "Ultimas Clases",
        table: {
            small: true, 
            titulos: [ {titulo: "Titulo"},  {titulo: "Ciclo"}  ], 
            filas: [
                    {datos: [{dato: "Primero A"},{dato: "Primer ciclo"}]},
                    {datos: [{dato: "Primero B"}, {dato: "Primer ciclo"}]},
                    {datos: [{dato: "Segundo A"},{dato: "Primer ciclo"}]},
                    {datos: [{dato: "Segundo B"},  {dato: "Primer ciclo"}]},
                    {datos: [{dato: "Tercero"},  {dato: "Primer ciclo"}]}
            ]
        }
    }
}


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


const Stats = {
    xs: 12, sm:12, md: 12, lg:12,
    background: "gray",
    goto: '/',
    header: {
        title: "Cantidad de alumnos por grado por curso",
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
}

export {Alumnos, Profesores, Materias, Clases, Stats};