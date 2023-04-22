const Administradores = {
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
            titulos: [ {titulo: "Nombre"}, {titulo: "Apellido"}, {titulo: "Colegio"} ], 
            filas: [
                    {datos: [{dato: "John"}, {dato: "Doe"}, {dato: "CREE"}]},
                    {datos: [{dato: "Mery"}, {dato: "Jane"}, {dato: "CEUCE"}]},
                    {datos: [{dato: "Pepe"}, {dato: "Lepew"}, {dato: "INMACULADA"}]},
                    {datos: [{dato: "Max"}, {dato: "Steele"}, {dato: "CIC"}]},
                    {datos: [{dato: "Laura"}, {dato: "Doe"}, {dato: "DIV. ESPERANZA"}]}
            ]
        }
    }
}

const Colegios = {
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
            titulos: [ {titulo: "Nombre"}, {titulo: "Estado"} ], 
            filas: [
                    {datos: [{dato: "Ceuce"}, {dato: "ACTIVO"}]},
                    {datos: [{dato: "Cree"},  {dato: "ACTIVO"}]},
                    {datos: [{dato: "Principito"}, {dato: "SIN ASIGNAR"}]},
                    {datos: [{dato: "Inmaculada"}, {dato: "ACTIVO"}]},
                    {datos: [{dato: "Morinigo"}, {dato: "INACTIVO"}]}
            ]
        }
    }

}


export {Colegios, Administradores};