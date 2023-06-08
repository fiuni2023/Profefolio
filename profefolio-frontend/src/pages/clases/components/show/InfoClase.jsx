import React, { useCallback, useEffect, useState } from 'react'
import { Form } from '../../../../components/Form';
import { ContainerBlock, STitle } from '../ShowsStyled';
import { useGeneralContext } from '../../../../context/GeneralContext';
import APILINK from '../../../../components/link';
import axios from 'axios';
import { toast } from 'react-hot-toast';
import { map } from "lodash"
import { useClaseContext } from '../../context/ClaseContext';
import ClassesService from '../../Helpers/ClassesHelper';
import Spinner from '../../../../components/componentsStyles/SyledSpinner';

const InfoClase = ({ idClase }) => {
    const { colegio, setColegio, getUserMail, getToken } = useGeneralContext();
    const [loading, setLoading] = useState(true);

    const [disabledInputs, setDisabledInputs] = useState(false);

    const { getClaseSelectedId } = useClaseContext()

    const [nombre, setNombre] = useState("");
    const [turno, setTurno] = useState("");
    const [ciclo, setCiclo] = useState("");
    const [anho, setAnho] = useState(0);

    const [ciclos, setCiclos] = useState([]);

    const getClase = useCallback(() => {
        axios.get(`${APILINK}/api/clase/${getClaseSelectedId()}`,
            {
                headers: {
                    Authorization: `Bearer ${getToken()}`
                }
            }).then((result) => {
                setNombre(result.data.nombre)
                setTurno(result.data.turno)
                setCiclo(result.data.idCiclo)
                setAnho(result.data.anho)
                setLoading(false);
            }).catch(() => {
                toast.error(`Error al obtener sus datos, recarge la pagina`);
            })
    }, [getToken, getClaseSelectedId])

    const getCiclos = useCallback(async () => {
        await axios.get(`${APILINK}/api/ciclo`,
            {
                headers: {
                    Authorization: `Bearer ${getToken()}`
                }
            }).then((result) => {
                setCiclos(result.data)

            }).catch(() => {
                toast.error(`Error al obtener sus datos, recarge la pagina`);
            })
    }, [getToken])

    const getInfoColegio = useCallback(async () => {
        const response = await axios.get(`${APILINK}/api/administrador/${getUserMail()}`,
            {
                headers: {
                    Authorization: `Bearer ${getToken()}`
                }
            })

        if (response.status === 200) {
            setColegio(response.data)
            return response.data
        } else {
            toast.error(`${response.error}`);
            return null;
        }
    }, [getToken, getUserMail, setColegio])

    useEffect(() => {
        getClase();
        getCiclos();
        if (colegio == null || colegio.id === 0 || "" === (colegio.nombre)) {
            getInfoColegio();
        }
    }, [colegio, getCiclos, getClase, getInfoColegio])


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


        try {
            const result = await ClassesService.updateClasse(getClaseSelectedId(), obj, getToken())

            toast.dismiss();
            if (result.status >= 200 && result.status < 300) {
                toast.success("Cambios guardados");
            } else {
                console.log(result?.error)
                toast.error(result.error);
            }
        } catch (e) {
            console.log(e)
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
                    default: "Seleccionar",
                    options: map(ciclos, (c) => ({ value: `${c.id}`, text: c.nombre }))
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
            <STitle>Editar Datos de la Clase</STitle>
            {loading ? <Spinner height={'60px'} /> : 
                <Form form={form} />
            }
        </ContainerBlock>
    </>
}

export default InfoClase