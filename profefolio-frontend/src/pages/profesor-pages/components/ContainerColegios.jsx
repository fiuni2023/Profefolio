import React from 'react'
import { map } from "lodash"
import Card from '../../../components/Card'
import { SRow } from '../../../components/componentsStyles/StyledDashComponent'


const ContainerColegios = ({
    onClick=()=>{},
    lista = []
}) => {

    const getColor = (pos) => {
        const colores = ["yellow", "blue", "purple", "orange"]
        // se obtienen siempre colores de las posiciones dentro del rango del array
        return colores[Math.abs(colores.length - pos) % (colores.length - 1)]
    }
    
    const getMateriasSubtitle = (materias = []) =>{
        let materiaLista = materias.length > 3? materias.slice(0,3) : materias
        return materias.length > 3? 
            `${map(materiaLista, (materia) => `${materia.nombre}`).join(", ")}, ...`
            :
            `${map(materiaLista, (materia) => `${materia.nombre}`).join(", ")}`
    }   


    const mapper = (objeto = {}, indice) => {
        return {
            xs: 12, sm: 12, md: 6, lg: 3,
            background: getColor(indice),
            hover: true,
            action: onClick,
            header: {
                title: `${objeto?.nombre}`,
            },
            body: {
                first: {
                    title: objeto?.clases ? 
                    `${objeto?.clases?.length} clases: ` 
                    : 
                    objeto?.materias? 
                    `${objeto?.materias?.length} Materias: ` 
                    :
                    objeto?.materia_anotaciones?
                    `${objeto.materia_anotaciones} Anotaciones`
                    :
                    "",

                    subtitle: 
                        objeto?.clases ? `${map(objeto?.clases, (clase) => `${clase.nombre}`).join(", ")}` 
                        : 
                        objeto?.materias? getMateriasSubtitle(objeto?.materias)
                        :
                        null
                },
                second: objeto?.alumnos ? {
                    title: `${objeto.alumnos} Alumnos`
                }
                :
                objeto?.materia_calif?
                {
                    title: `${objeto.materia_calif} Calificaciones`
                }
                :
                null,
                third: objeto?.materia_evento ? {
                    title: `${objeto.materia_evento} Eventos`
                }:null,
                schedule: {
                    main: `${map(objeto?.horario, (h) => `${h.dia} ${new Date(h.hora).toLocaleTimeString("en-EN", {
                        hour12: true,
                        hour: "numeric",
                        minute: "numeric",
                    })}`).join(" - ")}`,
                    secondary: objeto?.duracionHrs? objeto?.duracionHrs : null
                }
            }
        }
    }
    
    const colegios = map(lista, (colegio, i) => mapper(colegio, i))
    return <>
        <SRow>
            {colegios.map((element, i) => {
                if (element?.goto || element?.action ) return <Card key={i} cardInfo={element}></Card>
                else return 0
            })}
        </SRow>
    </>
}

export default ContainerColegios