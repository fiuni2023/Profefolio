import React, { useEffect } from 'react';
import { ScrollTable, SBody, SHeader, SForm, Container, Select, Item, List, ItemContainer, ListButton } from "./componentsStyles/StyledScrolleableList";
import TextButton from './TextButton';
import { RxReload } from 'react-icons/rx';

function ListItem({ index, name, lastName, document, type, onClick }) {
    return (
        <ItemContainer type={type == 'N' ? 'new' : type === 'D' ? 'reload' : '' }>
            <Item>{index}- {lastName} {name} - {document}</Item>
            <ListButton onClick={onClick}>{type !== 'D' ? 'X' : <RxReload style={{ fontSize: '24px' }} size={24} />}</ListButton>
        </ItemContainer>
    )
}


const Scrolleable = ({ studentsList, isLoading = true, handleSelectOption =()=>{}, handleDeleteStudent = ()=>{}}) => {

    useEffect(() => {
      
    console.log(studentsList)
    }, [studentsList])
    
    return (
        <Container>
            <ScrollTable>
                {studentsList?.header &&
                    <SHeader>
                        {studentsList?.header?.title}
                    </SHeader>}
                {studentsList?.list &&
                    <SBody background={studentsList?.background ?? "gray"}>
                        {isLoading ? <p>Cargando lista de alumnos</p>
                        : studentsList.list.length === 0 ? <p>No hay alumnos para mostrar</p>
                        : <List>
                            {studentsList?.list?.map((student, index) => (
                                <ListItem key={index}
                                    index={index + 1}
                                    id={student.alumnoId}
                                    name={student.nombre}
                                    lastName={student.apellido}
                                    document={student.documento}
                                    type={student.status}
                                    value={student.id}
                                    onClick={()=>handleDeleteStudent(student.id)} />
                            ))}
                        </List>}
                    </SBody>}
                <SForm onSubmit={studentsList?.onSubmit ?? null} >
                    <span>{studentsList?.addTitle}</span>
                    <Select defaultValue={""} onChange={handleSelectOption}>
                        <option value="" disabled>{studentsList?.selectTitle}</option>
                        {studentsList?.options?.map((option, index) => (
                            <option key={index} value={option.alumnoId}>
                                {option.apellido} {option.nombre} - {option.documento}
                            </option>
                        ))}
                    </Select>
                    <div style={{ textAlign: 'right' }}>
                        <TextButton buttonType={'save-changes'} enabled={studentsList?.enabled ?? false} />
                    </div>
                </SForm>
            </ScrollTable>
        </Container>
    )
}

export default Scrolleable; 