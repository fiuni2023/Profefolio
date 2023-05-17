import React from 'react'
import ContainerColegios from '../../components/ContainerColegios.jsx'
import { SRow } from '../../../../components/componentsStyles/StyledDashComponent.jsx'
import ShowContainer from '../../../clases/components/ShowContainer.jsx'
import Horarios from '../../components/Horarios/index.jsx'
import Eventos from '../../components/Eventos/index.jsx'
import { useGeneralContext } from '../../../../context/GeneralContext.jsx'
import { useModularContext } from '../../context/index.jsx'

const ProfesorPage = () => {
    const {getUserName} = useGeneralContext()
    const {setPage} = useModularContext()

    const handleClickCards = () => {
        setPage("clase")
    }

    const staticClases = [
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

    const componentes = {
        title: `Bienvenido Prof. ${getUserName()} Dashboard`,
        componentes: [
            <SRow>
                <ContainerColegios onClick={handleClickCards} lista={staticClases}/>
            </SRow>,
            <Horarios/>,
            <Eventos has_clase={true} has_colegio={true} />
        ]
    };
    return <>
        <ShowContainer data={componentes}/>
    </>
}

export default ProfesorPage