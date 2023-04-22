import React from 'react';
import { SCard, SBody, SHeader, SCol, SForm, Select, Item, List, ItemContainer, ListButton } from "./componentsStyles/StyledScrolleableList";
import Tabla from './Tabla';
import { useNavigate } from 'react-router';
import TextButton from './TextButton';
import { RxReload } from 'react-icons/rx';

function ListItem({ index, item, type, onClick }) {
    console.log(item)
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
    console.log(cardInfo)
    return (
        <SCol xs={cardInfo?.xs ?? 12} sm={cardInfo?.sm ?? 12} md={cardInfo?.md ?? 6} lg={cardInfo?.lg ?? 4}>
            <SCard onClick={cardInfo?.goto ? () => handleClick(cardInfo.goto) : null} hover={"false"}>
                {cardInfo?.header &&
                    <SHeader background={cardInfo?.background ?? "gray"}>
                        {cardInfo?.header?.title}</SHeader>}
                {cardInfo?.list &&
                    <SBody background={cardInfo?.background ?? "gray"}>
                        <List>
                            {cardInfo?.list?.map((student, index) => (
                                <ListItem
                                    index={index+1}
                                    item={student.name}
                                    type={student.status}
                                    onClick={() => console.log(`${student.name} 'seleccionado'`)} />
                            ))}
                        </List>
                    </SBody>}
                <SForm onSubmit={cardInfo?.onSubmit ?? null}>
                    <span>{cardInfo?.addTitle}</span>
                    <Select defaultValue={""}>
                        <option value="" disabled>{cardInfo?.selectTitle}</option>
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