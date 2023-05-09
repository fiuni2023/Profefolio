import React from 'react'
import { map } from "lodash"
import Card from '../../../components/Card'
import { SRow } from '../../../components/componentsStyles/StyledDashComponent'



const ContainerColegios = () => {

    const getColor = (pos) => {
        const colores = ["yellow", "blue", "purple", "orange"]
        // se obtienen siempre colores de las posiciones dentro del rango del array
        return colores[Math.abs(colores.length - pos) % (colores.length - 1)]
    }


    const mapper = (colegio = {}, indice) => {
        return {
            xs: 12, sm: 12, md: 6, lg: 3,
            background: getColor(indice),
            hover: true,
            goto: `/colegio/${colegio?.id}`,
            header: {
                title: `${colegio?.nombre}`,
            },
            body: {
                first: {
                    title: `${colegio?.clases?.length} clases: `,
                    subtitle: `${map(colegio?.clases, (clase) => `${clase.nombre}`).join(", ")}`
                },
                schedule: {
                    main: `${map(colegio?.horario, (h) => `${h.dia} ${new Date(h.hora).toLocaleTimeString("en-EN", {
                        hour12: true,
                        hour: "numeric",
                        minute: "numeric",
                    })}`).join(" - ")}`,
                }
            }
        }
    }
    const peticionResult = [
        {
            id: 1, //id colegio
            nombre: 'Colegio 1',
            clases: [
                {
                    id: 1, //id clase
                    nombre: "Primero A"
                },
                {
                    id: 2,
                    nombre: "Primero B"
                }
            ],
            horario: [
                {
                    id: 1, // id horario
                    dia: "Lunes",
                    hora: new Date()
                },
                {
                    id: 2,
                    dia: "Martes",
                    hora: new Date()
                }
            ]
        },
        {
            id: 2, //id colegio
            nombre: 'Colegio 2',
            clases: [
                {
                    id: 3, //id clase
                    nombre: "Segundo A"
                },
                {
                    id: 4,
                    nombre: "Primero E"
                }
            ],
            horario: [
                {
                    id: 4, // id horario
                    dia: "Martes",
                    hora: new Date()
                },
                {
                    id: 5,
                    dia: "Miercoles",
                    hora: new Date()
                }
            ]
        },
        {
            id: 3, //id colegio
            nombre: 'Colegio 3',
            clases: [
                {
                    id: 3, //id clase
                    nombre: "Segundo A"
                },
                {
                    id: 4,
                    nombre: "Primero E"
                }
            ],
            horario: [
                {
                    id: 4, // id horario
                    dia: "Martes",
                    hora: new Date()
                },
                {
                    id: 5,
                    dia: "Miercoles",
                    hora: new Date()
                }
            ]
        }
    ]

    const colegios = map(peticionResult, (colegio, i) => mapper(colegio, i))
    return <>
        <SRow>
            {colegios.map((element, i) => {
                if (element?.goto) return <Card key={i} cardInfo={element}></Card>
                else return 0
            })}
        </SRow>
    </>
}

export default ContainerColegios