/* eslint-disable */
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
import { toast } from 'react-hot-toast';


import { GrAddCircle } from 'react-icons/gr'


const TagNombreSelect = styled.div`
    border-radius: 20px;
    justify-content: space-around;
`;

const TagSelect = styled.select`
    background-color: #C2C2C2;
    padding: 0.2rem;
    width: fit-content;
    height: 30px;
    align-items: center;
    border-radius: 15px;
    justify-content: space-around;
`;

const TagTeacher = styled.div`
border-radius: 20px;
display: flex;
border: 1px solid black;
justify-content: space-around;
`;



const TagProfesor = memo(({ id, nombre,apellido, state = "new", onClick = () => { } }) => {
    const uid = useId();
    const unicId = uid.substring(1, uid.length - 1)

    const [type, setType] = useState("new");
    const bgColor = (estado) => {
        switch (`${estado.toLowerCase()}`) {
            case "reload":
                return "#F3E6AE";
            case "new":
                return "#D1F0E6";
            case "n":
               return "#F3E6AE";
        }
    }

    useEffect(() => {
        setType(bgColor(state))
    }, [state]);

    return <>
        <TagTeacher className={`tag-teacher-${unicId}`}>
            <Item className='item-nombre-profe'>{nombre} {apellido}</Item>
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
                width: fit-content;+
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



  

const ListItem = memo(({ index, idMateria, nombre,apellido, profesores = [] ,profeProfesor = [], type ,onClick,guardarProfesorSeleccionado,guardarProfesorSeleccionadoParaBorrar,guardarIdMateriaSeleccionado} ) => {


  const [idMateriaSeleccionado, setIdMateriaSeleccionado] = useState([]);
    const [profesoresSeleccionados, setProfesoresSeleccionados] = useState([]);

    const [profesoresMateriaSeleccionados, setProfesoresMateriaSeleccionados] = useState([]);

    const { setStatusProfesorMateria } = useClaseContext();

    const [isSelectOpen, setIsSelectOpen] = useState(false);
    
  const [usuariosSeleccionados, setUsuariosSeleccionados] = useState([]);


  const handleClick = () => {
    setIdMateriaSeleccionado(idMateria); 
    guardarIdMateriaSeleccionado(idMateriaSeleccionado);
  };

    const handleSelectOpenProfesores = () => {
        setIsSelectOpen(true);

      };

const seleccionarProfesor = (event) => {
  const idProfesorSeleccionado = event.target.value;

  setProfesoresSeleccionados((prevSeleccionados) =>
    prevSeleccionados.includes(idProfesorSeleccionado)
      ? prevSeleccionados.filter((id) => id !== idProfesorSeleccionado)
      : [...prevSeleccionados, idProfesorSeleccionado]
  );

  guardarProfesorSeleccionado(profesoresSeleccionados);
};

useEffect(() => {
    guardarIdMateriaSeleccionado(idMateriaSeleccionado);
  }, [idMateriaSeleccionado]);

  useEffect(() => {
    guardarProfesorSeleccionado(profesoresSeleccionados);
  }, [profesoresSeleccionados]);
  

  useEffect(() => {
    guardarProfesorSeleccionadoParaBorrar(usuariosSeleccionados);
  }, [usuariosSeleccionados]);

    return <>
        <ItemContainer type={type} className={`item-container-${index}`}>
            <div  onClick={handleClick}>
            <Item>{index}- {nombre} </Item>
                <div className={`profe-container-${index}`}>
                    <Item>Profesores:</Item>

                <ListButton >
                            <GrAddCircle
                                    onClick={handleSelectOpenProfesores}
                                    style={{ fontSize: "24px", color: "#C2C2C2" }}
                                    size={32}
        />

                    </ListButton>

                {isSelectOpen && (
                <TagNombreSelect>
                    <TagSelect onChange={seleccionarProfesor}>
                    {profeProfesor.map((profesor) => (
                        <option key={profesor.id} value={profesor.id}>
                        {profesor.nombre}
                        </option>
                    ))}
                    </TagSelect>
                </TagNombreSelect>
                )}

                {profeProfesor.map((profesor) => (
                    profesoresSeleccionados.includes(profesor.id) && (
                        <TagProfesor
                        key={profesor.id}
                        id={profesor.id}
                        nombre={`${profesor.nombre}${profesor.status}`}
                        apellido={`${profesor.apellido}${profesor.status}`}
                        state={profesor.status}
                        idMateriaProfesor={idMateria} 
                        onClick={() => {
                            setProfesoresSeleccionados((prevSeleccionados) =>
                            prevSeleccionados.filter((id) => id !== profesor.id)
                            );
                        }}
                        />
                    )
                    ))}

  {/* Este es un comentario en React*/}
         {map(profesores, (e, i) => 
         
         <TagProfesor 
         key={i} 
         id={e.idProfesor} 
         nombre={`${e.nombre}`} 
         apellido={`${e.apellido}`} 
         state={e.status} 
         idMateriaProfesor={idMateria} 
         onClick={() => {
            setStatusProfesorMateria(idMateria, e.id, e.status === "new" ? "reload" : "new");

            setUsuariosSeleccionados([...usuariosSeleccionados, e.idProfesor]);
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

    const [profeProfesor, setProfeProfesor] = useState([]);

    const [idMateria , setIdMateria]= useState("");
    const [idMateriaSeleccionada, setIdMateriaSeleccionada] = useState(null);

    const [idMateriaProfesores , setIdMateriaProfesores]= useState("");

    const [materiaProfesor, setMateriaProfesor] = useState([]);


    const [selectProfesores, setSelectProfesores]= useState([]);

    const { getListaMaterias, setStatusMateria, getClaseSelectedId, setProfesoresOptions } = useClaseContext();
    const { getToken } = useGeneralContext();

    const [materiaProfesores, setMateriaProfesores] = useState([]);

    const [profesoresSeleccionados, setProfesoresSeleccionados] = useState([]);

    const handleClickProfesor = (materia) => {
        const nuevosProfesores = materia.profesores.map((profesor) => profesor.id);
        setProfesoresSeleccionados([...profesoresSeleccionados, ...nuevosProfesores]);
      };
      



  
      
     

    /**
     * 
     * Pedir profesores del colegio
     */



//trae profesores 

useMemo(() => {
   
    const response = ClassesService.getProfesores(getToken());
  
    if (response !== null) {
      response.then((dataList) => {
        setProfeProfesor(dataList ?? []);

      }).catch(e => {

        setProfeProfesor([]);
      })
    }
  
  // eslint-disable-next-line react-hooks/exhaustive-deps
  }, [getToken]);
  

 


  //handleActualizarMateriaProfesor
  const handleCrearMateriaProfesor = async () => {
    const idProfesoresArray = profesoresSeleccionados;
    const idProfesoresArrayBorrado = profesoresSeleccionadosBorrado;
    const idMateria = idMateriaSeleccionada;
  
    const body = {
      "idClase": getClaseSelectedId(),
      "materias": []
    };
  
    idProfesoresArray.forEach((idProfesor) => {
      const profesor = {
        "idProfesor": idProfesor,
        "estado": "N"
      };
  
      const materia = {
        "idMateria": idMateria,
        "profesores": [profesor]
      };
  
      body.materias.push(materia);
    });
  
    idProfesoresArrayBorrado.forEach((idProfesor) => {
      const profesor = {
        "idProfesor": idProfesor,
        "estado": "D"
      };
  
      const materia = {
        "idMateria": idMateria,
        "profesores": [profesor]
      };
  
      body.materias.push(materia);
    });
  
    ClassesService.createMateriaProfesor(body, getToken())
      .then(() => {
        toast.success("Los datos fueron enviados correctamente.");
        window.location.reload();
      })
      .catch(() => {
        toast.error("No se pudieron guardar los cambios. Intente de nuevo o recargue la página.");
      });
  };
  



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
        MateriasService.getMateriaNoAssigned(getClaseSelectedId(), getToken()).then((response) => {

            if (response !== null) {
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



    const addMateriaToList = (materia) => {
        const newMateria = {
            idMateria: Date.now().toString(),
            materia,
           // status: "new",
            profesores: {}
        };
        setMateriaProfesor((materiaProfesor) => [...materiaProfesor, newMateria]);

    };

    const handleSelectOptionMateria = (e) => {
      e.preventDefault();
      setOptionSelected(e.target.value);
    
      
        setIdMateria(e.target.value);
       //console.log(`Asigna la materia con id: ${e.target.value} a la clase con id: ${getClaseSelectedId()} `)
      
       const index = optionsMaterias.findIndex(a => a.value === parseInt(e.target.value))

       addMateriaToList(optionsMaterias[index].label);

     
       
       optionsMaterias[index].status = "selected";
       setOptionsMaterias([...optionsMaterias]);
       
       //cargar a la lista de materias principal
       setOptionSelected("")

       //addMateriaToList(optionsMaterias[index].label);

       

    }

    useEffect(() => {
      const listaMateriasProfesores = async () => {
        try {
          const dataList = await ClassesService.getMateriasProfesores(getClaseSelectedId(),getToken());
          setMateriaProfesores(dataList ?? []);        

        } catch (e) {
          setMateriaProfesores([]);
        }
      };
    
      listaMateriasProfesores();
    }, []);

   

    let listaFusionada;

    if (materiaProfesor) {
      listaFusionada = materiaProfesor.map(item => ({
        ...item,
        estado: "new"
      }));
    } else {
      listaFusionada = [];
    }
    
    if (materiaProfesores && materiaProfesores.data && Array.isArray(materiaProfesores.data.materiaProfesores)) {
      const nuevosElementos = materiaProfesores.data.materiaProfesores.map(item => ({
        ...item,
        estado: "n"
      }));
      listaFusionada = [...listaFusionada, ...nuevosElementos];
    }

    /* let listaFusionada = [...materiaProfesor];

    if (Array.isArray(materiaProfesor)) {

    if (materiaProfesores && materiaProfesores.data && Array.isArray(materiaProfesores.data.materiaProfesores)) {
      listaFusionada = [...listaFusionada, ...materiaProfesores.data.materiaProfesores];
    }
    
}*/
    console.log('materiaProfesores',materiaProfesor);
    console.log('listaFusionada',listaFusionada);



//let listaFusionada = [...materiaProfesor];

/*if (materiaProfesores && materiaProfesores.data && Array.isArray(materiaProfesores.data.materiaProfesores)) {
  listaFusionada = [...listaFusionada, ...materiaProfesores.data.materiaProfesores];
}*/ 

/*let listaFusionada = [...materiaProfesor];

if (materiaProfesores && materiaProfesores.data && Array.isArray(materiaProfesores.data.materiaProfesores)) {
  listaFusionada = [...listaFusionada, ...materiaProfesores.data.materiaProfesores];
}*/

console.log('listaFusionada',listaFusionada);
const [profesoresSeleccionadosBorrado, setProfesoresSeleccionadosBorrado] = useState([]);

    const guardarProfesorSeleccionado = (profesoresSeleccionados) => {
        setProfesoresSeleccionados(profesoresSeleccionados);

    };

    const guardarProfesorSeleccionadoParaBorrar=(profesoresSeleccionadosBorrado) => {
        setProfesoresSeleccionadosBorrado(profesoresSeleccionadosBorrado);

    };

    const guardarIdMateriaSeleccionado=(idMateriaSeleccionada)=> {
        setIdMateriaSeleccionada(idMateriaSeleccionada);


    }


    /*const nuevaListaFusionada = listaFusionada.map(materia => {
        const nuevosProfesores = materia.profesores.filter(profesor => {
          return !profesoresSeleccionadosBorrado.includes(profesor.idProfesor);
        });
      
        return { ...materia, profesores: nuevosProfesores };
      });
      */
     /* useEffect(() => {
       
      }, [nuevaListaFusionada]);

      console.log('nuevaListaFusionada',nuevaListaFusionada);
    */
    let materiasList = {
        onSubmit: () => handleClickProfesor(materia),
        enabled: true,
        header: {
            title: "Lista de Materias de la Clase",
        },
        addTitle: "Agregar Materias",
        selectTitle: "Seleccionar Materia",
        options: optionsMaterias,
        //list: materiaProfesores.data?.materiaProfesores ?? [],
        // list: materiaProfesores.data.materiaProfesores ?? [],
        list:  listaFusionada ?? [],
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
                           {Array.isArray(materiasList?.list) && materiasList.list.map((materia, index) => (
                                <ListItem key={index}
                                    idMateria={materia.idMateria}
                                    index={index + 1}
                                    nombre={materia.materia}
                                    profesores={materia.profesores}
                                    profeProfesor={profeProfesor}
                                    type={materia.estado}
                                    guardarProfesorSeleccionado={guardarProfesorSeleccionado}
                                    guardarProfesorSeleccionadoParaBorrar={guardarProfesorSeleccionadoParaBorrar}
                                    guardarIdMateriaSeleccionado={guardarIdMateriaSeleccionado}

                                    onClick={(event) => {
                                        console.log(
                                          `${materia.nombre} seleccionado`,
                                          `${materia.id} seleccionado`,
                                          "profesores",
                                          materia.profesores.map((profesor) => profesor.id),
                                          "profeProfesor",
                                          profeProfesor
                                        );
                                        setIdMateria(materia.id); // Guardar el ID de la materia
                                        setIdMateriaProfesores(index);
                                        setSelectProfesores(profeProfesor);
                                        setStatusMateria(
                                          materia.id,
                                          materia.status === "new" ? "reload" : "new"
                                        );
                                        setIdMateriaSeleccionada(materia.id);
                                      }}
                                    />

                            ))}
                        </List>
                    </SBody>}
        
                <SForm onSubmit={materiasList?.onSubmit ?? null} >
                    <span>{materiasList?.addTitle}</span>
                    <Select value={optionSelected} onChange={(e) => { handleSelectOptionMateria(e) }}>
                        <option value="" disabled>{materiasList?.selectTitle}</option>
                        {materiasList?.options.filter(option => option.status === "not_used").length > 0 ? 
                        materiasList?.options.map((option, index) => (
                            option.status === "not_used" && <option key={index} value={option.value}>
                            {option.label}
                            </option>
                        )) :
                        <option value="" disabled>No hay materias sin asignar</option>
                        }
                    </Select>
                    <div style={{ textAlign: 'right' }}>
                                    <TextButton 
                                buttonType={'save-changes'} 
                                enabled={materiasList?.enabled ?? false} 
                                onClick={(e) => { 
                                e.preventDefault(); // prevent the default behavior of the onClick event
                                 handleCrearMateriaProfesor(); 
                  ///  console.log("enviando..."); // or alert("enviando...") to display the message to the user
                }} 
                />

                  
                  
                    </div>
                </SForm>
            </ScrollTable>
        </Container>

    </>
}

export default MateriasDeClase