import React, { useEffect, useState } from 'react'
import { Form } from '../../../../components/Form';
import { ContainerBlock, STitle } from '../ShowsStyled';
import { useGeneralContext } from '../../../../context/GeneralContext';
import APILINK from '../../../../components/link';
import axios from 'axios';
import { toast } from 'react-hot-toast';



const InfoClase = ({ idClase }) => {
    const { colegio, setColegio, getUserMail, getToken } = useGeneralContext();

    const [disabledInputs, setDisabledInputs] = useState(false);

    const [nombre, setNombre] = useState("");
    const [turno, setTurno] = useState("");
    const [ciclo, setCiclo] = useState("");
    const [anho, setAnho] = useState(0);

    const getClase = () => {
        
    }

    useEffect(() => {
        (async () => {
            const result = await axios.get(`${APILINK}/api/administrador/${getUserMail()}`,
                {
                    headers: {
                        Authorization: `Bearer ${getToken()}`
                    }
                })
            if (result.status === 200) {
                setColegio(result.data)
            }
            else {
                toast.error(`Error al obtener sus datos, recarge la pagina`);
            }
        })()
    }, [getToken, getUserMail, setColegio])


    const handleSudmit = async (e) => {
        e.preventDefault()

        const obj = {
            "colegioId": colegio.id,
            "cicloId": parseInt(ciclo),
            "nombre": nombre,
            "turno": turno,
            "anho": anho
        }

        toast("Enviando")
        setDisabledInputs(true)

        const result = await axios.post(`${APILINK}/api/clase/${idClase}`, obj, {
            headers: {
                Authorization: `Bearer ${getToken()}`,
            }
        })

        toast.dismiss();
        if(result.status === 200){
            toast.success("Cambios guardados");
        }else{
            toast.success(result.error);
        }
        setDisabledInputs(false)
    }

    const onChangeNombre = (event) => {
        if (`${event.target.value}`.length <= 128) {
            setNombre(`${event.target.value}`);
        }
    }

    const onChangeTurno = (event) => {
        if (`${event.target.value}`.length <= 32) {
            setTurno(`${event.target.value}`);
        }
    }

    const onChangeCiclo = (event) => {
        setCiclo(event.target.value);
    }

    const onChangeAnho = (event) => {
        const value = parseInt(event.target.value);
        if (!isNaN(value)) {
            setAnho(value > 0 ? value : 0);
        } else {
            setAnho(0)
        }
    }


    const form = {
        onSubmit: { action: handleSudmit },
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
                value: turno,
                placeholder: "Turno",
                disabled: disabledInputs,
                required: true,
                onChange: { action: onChangeTurno }
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
                value: ciclo,
                onChange: { action: onChangeCiclo },
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
                value: anho,
                label: "Año Lectivo",
                type: "number",
                placeholder: "2023",
                disabled: disabledInputs,
                required: true,
                onChange: { action: onChangeAnho }
            }
        ],
        buttons: [
            {
                style: "text",
                type: "save-changes",
                onclick: { action: () => { } },
                enabled: !disabledInputs
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