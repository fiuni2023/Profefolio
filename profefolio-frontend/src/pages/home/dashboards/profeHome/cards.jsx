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

export {colegios, clases, materias, infoClase};