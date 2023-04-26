
import React, { memo, useEffect, useId, useMemo, useState } from 'react'
import { Container, Item, ItemContainer, List, ListButton, SBody, SForm, SHeader, ScrollTable, Select } from '../../../../components/componentsStyles/StyledScrolleableList';
import { RxReload } from 'react-icons/rx';
import TextButton from '../../../../components/TextButton';
import { map } from "lodash"
import styled from 'styled-components';
import { useClaseContext } from '../../context/ClaseContext';
import MateriasService from "../../Helpers/MateriasHelper.js"
import { useGeneralContext } from '../../../../context/GeneralContext';
import ClassesService from '../../Helpers/ClassesHelper';


import { GrAddCircle } from 'react-icons/gr'



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
                max-width: calc(10rem - 24px);
                font-size: 15px;
                display: flex;
                align-items: center;
            }
            .tag-teacher-${unicId}{
                background-color: ${type};
                padding: 0.2rem;
                max-width: 10rem;
                width: fit-content;
                height: 24px;
                display: flex;
                align-items: center;
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
    const { setStatusProfesorMateria } = useClaseContext();

    const [isSelectOpen, setIsSelectOpen] = useState(false);

    const handleSelectOpen = () => {
        setIsSelectOpen(true);

        console.log('entroooo');
      };


    return <>
        <ItemContainer type={type} className={`item-container-${index}`}>
            <div>
            <Item>{index}- {nombre}</Item>
                <div className={`profe-container-${index}`}>
                    <Item>Profesores:</Item>

                <ListButton >
                            <GrAddCircle
                                    onClick={handleSelectOpen}
                                    style={{ fontSize: "24px", color: "#C2C2C2" }}
                                    size={32}
        />

                    </ListButton>

                    {isSelectOpen ? (
                    <Select>
                        {profesores.map((profesor) => (
                        <option key={profesor.id} value={profesor.id}>
                            {profesor.nombre}
                        </option>
                        ))}
                    </Select>
                    ) : null}


                    {map(profesores, (e, i) => <TagProfesor key={i} id={e.id} nombre={`${e.nombre}${e.status}`} state={e.status} onClick={() => {
                        setStatusProfesorMateria(idMateria, e.id, e.status === "new" ? "reload" : "new");
                    }
                    } />)}
                </div>
            </div>

            <ListButton onClick={onClick}>{type !== 'reload' ? 'X' : <RxReload style={{ fontSize: '24px' }} size={24} />} </ListButton>
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
    const [optionSelected, setOptionSelected] = useState("");
    const [optionsMaterias, setOptionsMaterias] = useState([]);

   
      


    const { getListaMaterias, setStatusMateria, getClaseSelectedId, addMateriaToList, setProfesoresOptions } = useClaseContext();
    const { getToken } = useGeneralContext();


    /**
     * 
     * Pedir profesores del colegio
     */

    useMemo(() => {
        //console.log("obtenido profes..")
        const response = ClassesService.getProfesoresParaClase(getToken());
        if (response !== null) {
            response.then((r) => {
                setProfesoresOptions(r.data ?? [])
            }).catch(e => {
                console.log(e)
                setProfesoresOptions([])
            })
        }

    // eslint-disable-next-line react-hooks/exhaustive-deps
    }, [getToken])


    
    
    /*    
    pedir materias con clases  
    
    
    */


    // pedir materias no asignadas a cla clase
    useMemo(() => {
        //console.log("obteniendo materias..")
        MateriasService.getMateriaNoAssigned(getClaseSelectedId(), getToken()).then((response) => {

            if (response !== null) {
                //console.log(response.data)
                setOptionsMaterias(map(response.data, (e, i) => ({
                    label: e.nombre_Materia,
                    value: e.id,
                    status: "not_used"
                })))
            }
            else {
                setOptionsMaterias([])
            }
        }).catch((e) => {
            setOptionsMaterias([])
            console.log(e)
        })

    }, [getClaseSelectedId, getToken])



    const handleSelectOptionMateria = (e) => {
        e.preventDefault();
        setOptionSelected(e.target.value)
        console.log(`Asigna la materia con id: ${e.target.value} a la clase con id: ${getClaseSelectedId()}`)

        if (/^[0-9]+$/.test(e.target.value)) {
            const index = optionsMaterias.findIndex(a => a.value === parseInt(e.target.value))

            addMateriaToList(optionsMaterias[index].label)
            optionsMaterias[index].status = "selected";

            setOptionsMaterias([...optionsMaterias]);
            //cargar a la lista de materias principal
            setOptionSelected("")
        }
    }

    let materiasList = {
        onSubmit: () => console.log("Guardado"),
        enabled: true,
        header: {
            title: "Lista de Materias de la Clase",
        },
        addTitle: "Agregar Materias",
        selectTitle: "Seleccionar Materia",
        options: optionsMaterias,
        list: getListaMaterias()
    }

    return <>

        <Container>
            <ScrollTable>
                {materiasList?.header &&
                    <SHeader>
                        {materiasList?.header?.title}
                    </SHeader>}

                {materiasList?.list &&
                    <SBody background={materiasList?.background ?? "gray"}>
                        <List>
                            {materiasList?.list?.map((materia, index) => (
                                <ListItem key={index}
                                    idMateria={materia.id}
                                    index={index + 1}
                                    nombre={materia.nombre}
                                    profesores={materia.profesores}
                                    type={materia.status}
                                    onClick={() => { console.log(`${materia.nombre} 'seleccionado'`); setStatusMateria(materia.id, (materia.status === "new" ? "reload" : "new")); }}
                                    
                                   
                                  
                                      />
                            ))}
                        </List>
                    </SBody>}

        
                <SForm onSubmit={materiasList?.onSubmit ?? null} >
                    <span>{materiasList?.addTitle}</span>
                    <Select value={optionSelected} onChange={(e) => { handleSelectOptionMateria(e) }}>
                        <option value="" disabled>{materiasList?.selectTitle}</option>
                        {map(materiasList?.options, (option, index) => (
                            option.status === "not_used" && <option key={index} value={option.value}>
                                {option.label}
                            </option>
                        ))}
                    </Select>
                    <div style={{ textAlign: 'right' }}>
                        <TextButton buttonType={'save-changes'} enabled={materiasList?.enabled ?? false} onClick={(e) => { "enviando..." }} />
                    </div>
                </SForm>
            </ScrollTable>
        </Container>

    </>
}

export default MateriasDeClase