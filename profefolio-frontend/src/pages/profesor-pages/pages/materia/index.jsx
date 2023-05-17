import React from "react";
import ContainerColegios from "../../components/ContainerColegios";
import { SRow } from "../../../../components/componentsStyles/StyledDashComponent";
import { useGeneralContext } from "../../../../context/GeneralContext";
import { useModularContext } from "../../context";
import ShowContainer from "../../../clases/components/ShowContainer";
import PromedioPuntaje from "../../components/PromedioPuntaje";
import PromedioAsistencia from "../../components/PromedioAsistencia";

const ProfesorMateria = () => {
    const {getUserName} = useGeneralContext()
    const {setPage} = useModularContext()

    const handleClickCards = () => {
        setPage("dashboard")
    }

    const staticCursos = [
        {
            id: 1, //id colegio
            nombre: 'Primer Grado',
            materias: [
                {
                    id: 1, //id clase
                    nombre: "Matematica"
                },
                {
                    id: 2,
                    nombre: "Castellano"
                },

            ],
            alumnos: 25,
            horario: [
                {
                    id: 1, // id horario
                    dia: "Lunes",
                    hora: new Date()
                }
            ],
            duracionHrs: "2hrs"
        },
        {
            id: 2, //id colegio
            nombre: 'Segundo Grado',
            materias: [
                {
                    id: 1, //id clase
                    nombre: "Matematica"
                },
                {
                    id: 2,
                    nombre: "Castellano"
                },

            ],
            alumnos: 25,
            horario: [
                {
                    id: 1, // id horario
                    dia: "Lunes",
                    hora: new Date()
                }
            ],
            duracionHrs: "2hrs"
        },
        {
            id: 3, //id colegio
            materias: [
                {
                    id: 1, //id clase
                    nombre: "Matematica"
                },
                {
                    id: 2,
                    nombre: "Castellano"
                },
                {
                    id: 3, //id clase
                    nombre: "Matematica2"
                },
                {
                    id: 4,
                    nombre: "Castellano2"
                },
            ],
            alumnos: 25,
            horario: [
                {
                    id: 1, // id horario
                    dia: "Lunes",
                    hora: new Date()
                }
            ],
            duracionHrs: "2hrs"
        }
    ]

    const componentes = {
        title: `Bienvenido Prof. ${getUserName()} Materia`,
        componentes: [
            <SRow>
                <ContainerColegios onClick={handleClickCards} lista={staticCursos}/>
            </SRow>,
            <PromedioPuntaje/>,
            <PromedioAsistencia/>
        ]
    };
    return (
        <>
            <ShowContainer data={componentes}/>
        </>
    )
}

export default ProfesorMateria