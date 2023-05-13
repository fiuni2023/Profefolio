import React, { useCallback, useEffect, useState } from 'react'
import { map } from "lodash"
import Card from '../../../components/Card'
import { SRow } from '../../../components/componentsStyles/StyledDashComponent'
import HorarioService from '../helpers/HorariosHelpers'
import { useGeneralContext } from '../../../context/GeneralContext'


const ContainerColegios = () => {

    const { getToken } = useGeneralContext();
    const [horariosColegios, setHorariosColegios] = useState([]);


    const getColor = (pos) => {
        const colores = ["yellow", "blue", "purple", "orange"]
        // se obtienen siempre colores de las posiciones dentro del rango del array
        return colores[Math.abs(colores.length - pos) % (colores.length - 1)]
    }
    

    const mapper = useCallback((colegio = {}, indice) => {
        return {
            xs: 12, sm: 12, md: 6, lg: 3,
            background: getColor(indice),
            hover: true,
            goto: `/colegio/${colegio?.id}`,
            header: {
                title: `${colegio?.nombre}`,
            },
            body: {
                first: {
                    title: `${colegio?.clases?.length} clases: `,
                    subtitle: `${colegio?.clases?.join(", ")}`
                },
                schedule: {
                    main: `${map(colegio?.horarios, (h) => `${h.dia} ${h.inicio}`).join(" - ")}`,
                }
            }
        }
    }, [])



    useEffect(() => {
        const getHorarios = async () => {
            
            const result = await HorarioService.getColegiosAndHorarios(getToken());
            if (result !== null) {
                setHorariosColegios(map(result, (r, i) => mapper(r, i)))
            } else {
                console.log("Error al obtener el horario")
            }
        }
        
        getHorarios();
        
    }, [getToken, mapper]);

    return <>
        <SRow>
            {horariosColegios 
                ? horariosColegios.length === 0 
                ? <h5>No tiene Clases asignadas</h5> 
                : horariosColegios.map((element, i) => {
                    if (element?.goto) return <Card key={i} cardInfo={element}></Card>
                    else return 0
                }) 
                : <h5>Hubo un problema, recargue la pagina o 
                    comuniquese con el Administrador de su colegio para verificar 
                    que tenga clases asignadas</h5>  }
        </SRow>
    </>
}

export default ContainerColegios