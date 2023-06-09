import React, {useState} from "react";
import { useModularContext } from "../../context";
import { Form } from "../../../../components/Form";
import { H2 } from "../../../../components/componentsStyles/StyledModal";
import { SRow2 } from "../../../../components/componentsStyles/StyledForm";
import EventosService from "../../helpers/EventosHelpers";
import { useGeneralContext } from "../../../../context/GeneralContext";
import { toast } from "react-hot-toast";

const CreateEvent = () => {
    const {dataSet, stateController} = useModularContext()
    const {materiasSelect} = dataSet
    const {colegioId, claseId} = stateController
    const [currEvent, setEvent] = useState(undefined)
    const [currMateria, setCurrMateria] = useState(undefined)
    const [currDate, setCurrDate] = useState(undefined)
    const {getToken} = useGeneralContext();

    const handleEventChange = (event) => {
        setEvent(event.target.value); 
    }

    const handleMateriaChange = (event) => {
        setCurrMateria(parseInt(event.target.value)); 
    }

    const handleDateChange = (event) => {
        let formatDate = new Date(event.target.value)
        try{
            setCurrDate(formatDate.toISOString())
        } catch (e){
        }
    }

    const handleSubmit = async () => {
        if (currEvent !== undefined && currMateria !== undefined && currDate !== undefined) {
            const body = JSON.stringify({
                tipo: currEvent,
                fecha: currDate,
                materiaId: currMateria,
                claseId: claseId,
                colegioId: colegioId
            })
            try {
                const response = await EventosService.postEvento(body, getToken());
                if (response === null) {
                    toast.error("No se pudo crear el evento")
                } else {
                    document.getElementById("My-form").reset();
                    setCurrDate(undefined);
                    setCurrMateria(undefined);
                    setEvent(undefined);
                    toast.success("Evento creado")
                }
            } catch (error) {
                if (typeof error.response.data === "string") toast.error(error.response.data);
                else toast.error("No se pudo crear el evento")
            }
        } else {
            toast.error("Todos los campos son necesarios")
        }
    }
    
    const form = {
        preventDefault: true,
        inputs : [
            {
                xs: 12, sm: 12, md: 6, lg: 6,
                key: 'event-materia',
                type: 'select', label: 'Materia:',
                onChange: {action: handleMateriaChange},
                select: { 
                    default: "Seleccione una opcion",
                    options: materiasSelect},
                text: "Seleccione una materia de éste colegio",
            }, 
            {
                xs: 12, sm: 12, md: 6, lg: 6,
                key: 'event-type', label: 'Tipo de evento:',
                type: 'select', 
                onChange: {action: handleEventChange},
                select: { 
                    default: "Seleccione una opcion",
                    options: [
                        {value: 'Evento',  text: 'Evento'},
                        {value: 'Parcial', text: 'Parcial'},
                        {value: 'Prueba sumatoria',  text: 'Prueba sumatoria'},
                        {value: 'Examen', text: 'Examen'},
                    ]
                }
            }, 
            {
                key: 'event-date', label: 'Fecha:',
                onChange: {action: handleDateChange},
                type: 'date',
                invalidText: 'Seleccione una fecha'
            }
        ],
        buttons: [{style: "text", type: "save", onclick: { action: handleSubmit}}]
    }

    return (
        <>
        {materiasSelect && materiasSelect.length > 0 && 
            <SRow2>
                <H2>Crear nuevo evento:</H2>
                <Form form={form}></Form>
            </SRow2>
        }
        </>
    )
}

export default CreateEvent;