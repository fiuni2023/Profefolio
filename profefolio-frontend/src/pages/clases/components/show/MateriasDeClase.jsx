import React from 'react'
import { Container, Item, ItemContainer, List, ListButton, SBody, SForm, SHeader, ScrollTable, Select } from '../../../../components/componentsStyles/StyledScrolleableList';
import { RxReload } from 'react-icons/rx';
import TextButton from '../../../../components/TextButton';
import {map} from "lodash"

const ListItem = ({index, nombre, profesores=[], type, onClick}) => {
    return (
        <ItemContainer type={type}>
            <div>
            <Item>{index}- {nombre}</Item>
            <div>
                <span>Profesores: </span>
                {map(profesores, (e, i) => <div key={i}>{e.nombre}</div>)}
            </div>
            </div>

            <ListButton onClick={onClick}>{type !== 'reload' ? 'X' : <RxReload style={{ fontSize: '24px' }} size={24} />}</ListButton>
        </ItemContainer>
    )
}
const MateriasDeClase = () => {
    const clasesList = {
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
        list: [
            { id: 1, nombre: "Matematicas", profesores: [{id: 1, nombre: "John Foe"}] },
            { id: 2, nombre: "Matematicas", profesores: [{id: 2, nombre: "John Foe"}] },
            { id: 3, nombre: "Matematicas", profesores: [{id: 3, nombre: "John Foe"}] },
            { id: 4, nombre: "Matematicas", profesores: [{id: 4, nombre: "John Foe"}] },
            { id: 5, nombre: "Matematicas", profesores: [{id: 5, nombre: "John Foe"}] },
            { id: 6, nombre: "Matematicas", profesores: [{id: 6, nombre: "John Foe"}] },
            { id: 7, nombre: "Matematicas", profesores: [{id: 7, nombre: "John Foe"}] },
            { id: 8, nombre: "Matematicas", profesores: [{id: 8, nombre: "John Foe"}] },
            { id: 9, nombre: "Matematicas", profesores: [{id: 9, nombre: "John Foe"}] },
            { id: 10, nombre: "Matematicas", profesores: [{id: 10, nombre: "John Foe"}] },
            { id: 11, nombre: "Matematicas", profesores: [{id: 11, nombre: "John Foe"}] },
            { id: 12, nombre: "Matematicas", profesores: [{id: 12, nombre: "John Foe"}] },
        ]
    }
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