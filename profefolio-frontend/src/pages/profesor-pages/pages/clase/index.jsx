import React from "react";
import ContainerColegios from "../../components/ContainerColegios";
import Horarios from "../../components/Horarios";
import Eventos from "../../components/Eventos";
import { SRow } from "../../../../components/componentsStyles/StyledDashComponent";
import { useGeneralContext } from "../../../../context/GeneralContext";
import { useModularContext } from "../../context";
import ShowContainer from "../../../clases/components/ShowContainer";
import BackButton from "../../components/BackButton";

const ProfesorClase = () => {
    const {getUserName} = useGeneralContext()
    const {setPage} = useModularContext()

    const handleClickCards = () => {
        setPage("materia")
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
            nombre: 'Tercer Grado',
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
        title: `Bienvenido Prof. ${getUserName()} Clase`,
        componentes: [
            <SRow>
                <ContainerColegios onClick={handleClickCards} lista={staticCursos}/>
            </SRow>,
            <Horarios/>,
            <Eventos/>
        ]
    };
    return (
        <>
            <BackButton to="dashboard" />
            <ShowContainer data={componentes}/>
        </>
    )
}

export default ProfesorClase