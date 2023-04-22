import React from 'react';
import { SCard, SBody, SHeader, SCol, SForm, Select, Item, List, ItemContainer, ListButton } from "./componentsStyles/StyledScrolleableList";
import Tabla from './Tabla';
import { useNavigate } from 'react-router';
import TextButton from './TextButton';
import { RxReload } from 'react-icons/rx';

function ListItem({ index, item, type, onClick }) {
    return (
        <ItemContainer type={type}>
            <Item>{index}- {item}</Item>
            <ListButton onClick={onClick}>{type!=='reload'? 'X' : <RxReload style={{fontSize: '24px'}} size={24} />}</ListButton>
        </ItemContainer>
    )
}


function Scrolleable({ cardInfo }) {
    const nav = useNavigate();
    const handleClick = (goto) => {
        nav(goto);
    }
    return (
        <SCol xs={cardInfo?.xs ?? 12} sm={cardInfo?.sm ?? 12} md={cardInfo?.md ?? 6} lg={cardInfo?.lg ?? 4}>
            <SCard onClick={cardInfo?.goto ? () => handleClick(cardInfo.goto) : null} hover={"false"}>
                {cardInfo?.header &&
                    <SHeader background={cardInfo?.background ?? "gray"}>
                        {cardInfo?.header?.title}</SHeader>}
                {cardInfo?.body &&
                    <SBody background={cardInfo?.background ?? "gray"}>
                        {cardInfo?.body?.table && <Tabla datosTabla={cardInfo?.body?.table}></Tabla>}

                        <List>
                            <ItemContainer>
                                <Item>First list item</Item>
                            </ItemContainer>
                            <ItemContainer  type={'reload'}>
                                <Item>Second list item</Item>
                            </ItemContainer>
                            <ItemContainer type={'new'}>
                                <Item >Last list item</Item>
                                <ListButton>X</ListButton>
                            </ItemContainer>
                            <ListItem 
                            index={5}
                            item={'Claudia Martinez'}
                            type={'new'}
                            onClick={()=>console.log('first')}/>
                            <ListItem 
                            index={6}
                            item={'Claudia Martinez'}
                            type={'reload'}
                            onClick={()=>console.log('first')}/>
                            <ListItem 
                            index={7}
                            item={'Claudia Martinez'}
                            onClick={()=>console.log('first')}/>
                        </List>
                    </SBody>}
                <SForm onSubmit={cardInfo?.onSubmit ?? null}>
                    <span>Agregar alumnos</span>
                    <Select aria-label="Default select">
                        <option value="" disabled selected>Seleccione un alumno</option>
                        {cardInfo?.options?.map((option, index) => (
                            <option key={index} value={option.value}>
                                {option.label}
                            </option>
                        ))}
                    </Select>

                    <div style={{ textAlign: 'right' }}>
                        <TextButton buttonType={'save-changes'} enabled={cardInfo?.enabled ?? false} />
                    </div>
                </SForm>
            </SCard>
        </SCol>
    )
}

export default Scrolleable; 