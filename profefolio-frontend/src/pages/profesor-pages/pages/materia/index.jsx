import React from "react";
import { SRow } from "../../../../components/componentsStyles/StyledDashComponent";
import { useGeneralContext } from "../../../../context/GeneralContext";
import { useModularContext } from "../../context";
import ShowContainer from "../../../clases/components/ShowContainer";
import PromedioPuntaje from "../../components/PromedioPuntaje";
import PromedioAsistencia from "../../components/PromedioAsistencia";
import MateriaCards from "../../components/MateriaCards";

const ProfesorMateria = () => {
    const {getUserName} = useGeneralContext()
    const {setPage} = useModularContext()

    const handleClickCards = () => {
        setPage("dashboard")
    }


    const config = {
        onAnotation: handleClickCards
    }

    const componentes = {
        title: `Bienvenido Prof. ${getUserName()} Materia`,
        componentes: [
            <SRow>
                <MateriaCards configuration={config} />
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