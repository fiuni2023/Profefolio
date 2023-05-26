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
            `${map(materiaLista, (materia) => `${materia}`).join(", ")}, ...`
            :
            `${map(materiaLista, (materia) => `${materia}`).join(", ")}`
    }   

    const mapper = (objeto = {}, indice) => {
        return {
            xs: 12, sm: 12, md: 6, lg: 3,
            background: getColor(indice),
            hover: true,
            action: ()=>onClick(objeto?.id),
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
                    objeto?.anotaciones?
                    `${objeto.anotaciones} Anotaciones`
                    :
                    objeto?.anotaciones === 0?
                    `${objeto.anotaciones} Anotaciones`
                    :
                    "",

                    subtitle: 
                        objeto?.clases ? `${map(objeto?.clases, (clase) => `${clase}`).join(", ")}` 
                        : 
                        objeto?.materias? getMateriasSubtitle(objeto?.materias)
                        :
                        null
                },
                
                //-----------------------ciclo--------------------------------
                second: objeto?.alumnos ? {
                    title: `${objeto.alumnos} Alumnos`
                }
                :
                objeto?.calificaciones?
                {
                    title: `${objeto.calificaciones} Calificaciones`
                }
                :
                objeto?.calificaciones === 0?
                {
                    title: `${objeto.calificaciones} Calificaciones`
                }
                :
                objeto?.ciclo?
                {
                    title: `Ciclo: ${objeto.ciclo}`,
                }
                :
                null,

                //-----------------------eventos--------------------------------
                third: objeto?.eventos ? {
                    title: `${objeto.eventos} Eventos`
                }
                :
                objeto?.eventos === 0? {
                    title: `${objeto.eventos} Eventos`
                }
                :
                null,
                //-----------------------horas--------------------------------
                schedule: {
                    main: objeto?.horario? 
                        objeto.horario[0]? `${objeto.horario[0].dia} ${objeto.horario[0].inicio}` : `${objeto.horario.dia} ${objeto.horario.inicio}`
                    :
                    objeto?.horarios?
                    objeto.horarios.map(h=>{return `${h.dia} ${h.inicio}`}).join(' - ')
                    : 
                    "No hay un horario todavia",
                    secondary: objeto?.horario?.horas? objeto?.horario?.horas : null
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