import React from "react";
import { SRow } from "../../../../components/componentsStyles/StyledDashComponent";
import { useGeneralContext } from "../../../../context/GeneralContext";
import { useModularContext } from "../../context";
import ShowContainer from "../../../clases/components/ShowContainer";
import BackButton from "../../components/BackButton";
import Alumnos from "../../components/Alumnos";
import Eventos from "../../components/Eventos";
import ContainerColegios from "../../components/ContainerColegios";

const ProfesorMateria = () => {
    const {getUserName} = useGeneralContext()
    const {setPage} = useModularContext()

    const handleClickCards = () => {
        setPage("materiashow")
    }

    const staticCursos = [
        {
            id: 1, //id colegio
            nombre: 'Ciencias',
            materia_anotaciones: 23,
            materia_calif: 4,
            materia_evento: 2,
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
            nombre: 'Matematicas',
            materia_anotaciones: 23,
            materia_calif: 4,
            materia_evento: 2,
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
            nombre: 'Castellano',
            materia_anotaciones: 23,
            materia_calif: 4,
            materia_evento: 2,
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
            <Alumnos />,
            <Eventos />
        ]
    };
    return (
        <>  
            <BackButton to="clase" />
            <ShowContainer data={componentes}/>
        </>
    )
}

export default ProfesorMateria