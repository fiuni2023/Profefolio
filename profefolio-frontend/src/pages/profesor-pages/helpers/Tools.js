import { find, map } from "lodash"

const idsWithColor = [];



const compararStringsSinAcentos = function (str1, str2) {
    // Normalizar los strings a Unicode NFD
    var str1Normalized = str1.normalize("NFD");
    var str2Normalized = str2.normalize("NFD");

    // Reemplazar los caracteres acentuados por sus equivalentes sin acento
    var str1SinAcentos = str1Normalized.replace(/[\u0300-\u036f]/g, "");
    var str2SinAcentos = str2Normalized.replace(/[\u0300-\u036f]/g, "");

    // Realizar la comparación entre los strings modificados
    return str1SinAcentos === str2SinAcentos;
};


const SelectColor = (color) => {
    switch (color) {
        case "bluesky":
            return "#C1E1FA";
        case "purple":
            return "#C8BFD9";
        case "yellow":
            return "#F6E7A7";
        case "orange":
            return "#FCC6AC";
        case "blue":
            return "#8DACE1";
        case "green":
            return "#59C8A4";
        case "pink":
            return "#F2BFD3";
        case "salmon":
            return "#F5918E"
        default:
            return "#C2C2C2"
    }
}

/**
 * Retorna un color para un id en particular y si recibe dos veces el mismo id, se retornara el mismo color del anterios
 */
const GetColors = (idElement) => {
    const listColorsName = ["bluesky", "yellow", "green", "orange", "blue", "purple", "pink", "salmon"]

    const value = find(idsWithColor, a => a.id === idElement)

    if (!!value) {
        return SelectColor(value.colorName)
    }
    else {
        const colorName = listColorsName[idsWithColor.length]
        const colorElement = { id: idElement, colorName: colorName }
        idsWithColor.push(colorElement)

        return SelectColor(colorName)
    }
}



// mappeos de eventos
const MapperHorariosByColegio = (eventos) => {
    const dias = ["Domingo", "Lunes", "Martes", "Miercoles", "Jueves", "Viernes", "Sabado"]

    return map(eventos, (e, i) => {
        const dia = dias.findIndex(a => compararStringsSinAcentos(a, e?.dia))

        const fecha = new Date()
        // Calcular la diferencia entre los días
        var diferenciaDias = dia - fecha.getDay();

        // Establecer el nuevo valor de día sumando la diferencia
        fecha.setDate(fecha.getDate() + diferenciaDias);

        const inicio = new Date(fecha);
        inicio.setHours(parseInt(e?.inicio?.split(":")[0]), parseInt(e?.inicio?.split(":")[1]), 0, 0);

        const fin = new Date(fecha);
        fin.setHours(parseInt(e?.fin?.split(":")[0]), parseInt(e?.fin?.split(":")[1]), 0, 0);
        const color = GetColors(e.colegioId)
        return {
            id: e.id,
            title: e.nombreColegio,
            start: inicio,
            end: fin,
            color: color
        }

    })
}

const GetColorEvento = (evento = "") => {
    switch (evento.toLowerCase()) {
        case "evento":
            return SelectColor("purple");
        case "examen":
            return SelectColor("yellow");
        case "parcial":
            return SelectColor("bluesky");
        case "prueba sumativa":
            return SelectColor("salmon");
        default:
            return "#DDDDDD";
    }
}

const Tools = { compararStringsSinAcentos, GetColors, MapperHorariosByColegio, SelectColor, GetColorEvento}

export default Tools