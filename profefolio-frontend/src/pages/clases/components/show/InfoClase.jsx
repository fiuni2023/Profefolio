import React, { useState } from 'react'
import { Form } from '../../../../components/Form';
import { ContainerBlock, STitle } from '../ShowsStyled';
import { H1} from '../../../../components/componentsStyles/StyledModal.jsx';



const InfoClase = ({ idClase }) => {
    const [disabledInputs, setDisabledInputs] = useState(false);

    const [nombre, setNombre] = useState("");
    const [turno, setTurno] = useState("");
    const [ciclo, setCiclo] = useState("");
    const [anho, setAnho] = useState(0);

    const onChangeNombre = (nombre) => {
        console.log(nombre.target.value)
        setNombre(nombre.target.value);
    }
    const form = {
        inputs: [
            {
                xs: 6,
                sm: 6,
                md: 6,
                lg: 6,
                key: "1",
                label: "Nombre",
                type: "text",
                placeholder: "Nombre",
                disabled: disabledInputs,
                required: true,
                value: nombre,
                onChange: { action: onChangeNombre }
            },
            {
                xs: 6,
                sm: 6,
                md: 6,
                lg: 6,
                key: "2",
                label: "Turno",
                type: "text",
                placeholder: "Turno",
                disabled: disabledInputs,
                required: true,
                onChange: null
            },
            {
                xs: 6,
                sm: 6,
                md: 6,
                lg: 6,
                key: "3",
                label: "Ciclo",
                type: "select",
                placeholder: "Ciclo",
                disabled: disabledInputs,
                required: true,
                onChange: () => { },
                select: {
                    default: "Seleccione",
                    options: [
                        {
                            value: "1",
                            text: "Opcion 1"
                        }
                    ]
                }
            },
            {
                xs: 6,
                sm: 6,
                md: 6,
                lg: 6,
                key: "4",
                label: "Año Lectivo",
                type: "text",
                placeholder: "2023",
                disabled: disabledInputs,
                required: true,
                onChange: () => { }
            }
        ],
        buttons: [
            {
                style: "text",
                type: "save",
                onclick: () => { },
                enabled: true
            }
        ]
    }

    return <>
        <ContainerBlock>
            <STitle>Editar Datos del Grado</STitle>
            <Form form={form} />
        </ContainerBlock>
    </>
}

export default InfoClase