import React, { memo, useEffect, useId, useMemo, useState } from 'react'
import { Container, Item, ItemContainer, List, ListButton, SBody, SForm, SHeader, ScrollTable, Select } from '../../../../components/componentsStyles/StyledScrolleableList';
import { RxReload } from 'react-icons/rx';
import TextButton from '../../../../components/TextButton';
import { map } from "lodash"
import styled from 'styled-components';
import { useClaseContext } from '../../context/ClaseContext';

const TagTeacher = styled.div`
    border-radius: 20px;
    display: flex;
    border: 1px solid black;
    justify-content: space-around;
`

const TagProfesor = memo(({ id, nombre, state = "new", onClick = () => { } }) => {
    const uid = useId();
    const unicId = uid.substring(1, uid.length - 1)

    const [type, setType] = useState("new");
    const bgColor = (estado) => {
        switch (`${estado.toLowerCase()}`) {
            case "reload":
                return "#F3E6AE";
            case "new":
                return "#D1F0E6";
            default:
                return "#C2C2C2";
        }
    }

    useEffect(() => {
        setType(bgColor(state))
    }, [state]);
    const a = useMemo(() => {
        console.log(nombre + state)
        return nombre + state;
    }, [nombre, state])
    return <>
        <TagTeacher className={`tag-teacher-${unicId}`}>
            <Item className='item-nombre-profe'>{nombre}</Item>
            <ListButton onClick={onClick}>{state !== 'reload' ? 'X' : <RxReload style={{ fontSize: '24px' }} size={24} />}</ListButton>

        </TagTeacher>
        <style jsx="true">{
            `
            .item-nombre-profe{
                padding-left: 5px;
                overflow: hidden;
                text-overflow: ellipsis;
                white-space: nowrap;
                max-width: calc(10rem - 24px)
            }
            .tag-teacher-${unicId}{
                background-color: ${type};
                padding: 0.2rem;
                max-width: 10rem;
                width: fit-content;
            }
            .btn-cancelar{
                background-color: red;
                min-width: 1rem;
            }
        `
        }</style>
    </>
})

const ListItem = memo(({ index, idMateria, nombre, profesores = [], type, onClick }) => {
    const { setStatusMateria } = useClaseContext();

    return <>
        <ItemContainer type={type} className={`item-container-${index}`}>
            <div>
                <Item>{index}- {nombre}</Item>
                <div className={`profe-container-${index}`}>
                    <Item>Profesores:</Item>
                    {map(profesores, (e, i) => <TagProfesor key={i} id={e.id} nombre={`${e.nombre}${e.status}`} state={e.status} onClick={() => {
                        setStatusMateria(idMateria, e.id, e.status === "new" ? "reload" : "new");
                    }
                    } />)}
                </div>
            </div>

            <ListButton onClick={onClick}>{type !== 'reload' ? 'X' : <RxReload style={{ fontSize: '24px' }} size={24} />}</ListButton>
        </ItemContainer>

        <style jsx="true">
            {
                `
                    .item-container-${index}{
                        min-height: 6rem;
                        align-items: flex-start;
                    }
                    .profe-container-${index}{
                        display: flex;
                        flex-wrap: wrap;
                        column-gap: 0.5rem;
                        align-items: center;
                        margin-bottom: 0.5rem;
                        row-gap: 4px;
                    }
                `
            }
        </style>
    </>
})
const MateriasDeClase = () => {
    const { getListaMaterias } = useClaseContext();

    let clasesList = {
        onSubmit: () => console.log("Guardado"),
        enabled: true,
        header: {
            title: "Lista de Alumnos inscriptos",
        },
        addTitle: "Agregar alumnos",
        selectTitle: "Seleccionar alumno",
        options: [
            { label: "Carlos", value: 1 },
            { label: "Gabriela", value: 1 }
        ],
        list: getListaMaterias()
    }
    //console.log(clasesList.list)
    return <>

        <Container>
            <ScrollTable>
                {clasesList?.header &&
                    <SHeader>
                        {clasesList?.header?.title}
                    </SHeader>}
                {clasesList?.list &&
                    <SBody background={clasesList?.background ?? "gray"}>
                        <List>
                            {clasesList?.list?.map((clase, index) => (
                                <ListItem key={index}
                                    idMateria={clase.id}
                                    index={index + 1}
                                    nombre={clase.nombre}
                                    profesores={clase.profesores}
                                    type={clase.status}
                                    onClick={() => console.log(`${clase.nombre} 'seleccionado'`)} />
                            ))}
                        </List>
                    </SBody>}
                <SForm onSubmit={clasesList?.onSubmit ?? null} >
                    <span>{clasesList?.addTitle}</span>
                    <Select defaultValue={""}>
                        <option value="" disabled>{clasesList?.selectTitle}</option>
                        {clasesList?.options?.map((option, index) => (
                            <option key={index} value={option.value}>
                                {option.label}
                            </option>
                        ))}
                    </Select>
                    <div style={{ textAlign: 'right' }}>
                        <TextButton buttonType={'save-changes'} enabled={clasesList?.enabled ?? false} />
                    </div>
                </SForm>
            </ScrollTable>
        </Container>

    </>
}

export default MateriasDeClase