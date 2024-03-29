import React from "react";
import Card from '../../../../components/Card'
import { SRow } from '../../../../components/componentsStyles/StyledDashComponent'

const MateriaCards = ({
    configuration = {
        onAnotation: () => {},
        onAsistencia: () => {},
        onDocumento: () => {},
        onEvaluacion: () => {}
    },
    materia = {
        anotations: 15,
        name: "ciencias",
        calification_count: 4,
        event_yet: 2,
        classes_yet: 8,
        documents: 4
    }
}) => {
    const { onAnotation, onAsistencia, onDocumento, onEvaluacion } = configuration
    const configAnotacion = {
        xs: 12, sm: 12, md: 6, lg: 3,
        background: "yellow",
        hover: true,
        action: onAnotation,
        header: {
            title: `Anotaciones`,
        },
        body: {
            first: {
                title: `${materia?.anotations} Anotaciones para ${materia?.name}`
            }

        }
    }

    const configCalificacion = {
        xs: 12, sm: 12, md: 6, lg: 3,
        background: "blue",
        hover: true,
        action: onEvaluacion,
        header: {
            title: `Calificaciones`,
        },
        body: {
            first: {
                title: `${materia?.calification_count} calificaciones`
            }
        }
    }

    const configAsistencia = {
        xs: 12, sm: 12, md: 6, lg: 3,
        background: "purple",
        hover: true,
        action: onAsistencia,
        header: {
            title: `Asistencia`,
        },
        body: {
            first: {
                title: `${materia?.asistencias}% Promedio de Asistencias`
            },
            second: {
                title: `${materia?.classes_yet} clases del mes`
            }
        }
    }

    const configDocumento = {
        xs: 12, sm: 12, md: 6, lg: 3,
        background: "orange",
        hover: true,
        action: onDocumento,
        header: {
            title: `Documentos`,
        },
        body: {
            first: {
                title: `${materia?.documents} documentos para ${materia?.name}`
            }
        }
    }

    return <>
        <SRow>
            <Card cardInfo={configAnotacion}></Card>
            <Card cardInfo={configCalificacion}></Card>
            <Card cardInfo={configAsistencia}></Card>
            <Card cardInfo={configDocumento}></Card>
        </SRow>
    </>
}

export default MateriaCards