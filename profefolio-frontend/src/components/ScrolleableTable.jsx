import React from 'react';
import { ScrollTable, SBody, SHeader, SForm, Container, Select, Item, List, ItemContainer, ListButton } from "./componentsStyles/StyledScrolleableList";
import TextButton from './TextButton';
import { RxReload } from 'react-icons/rx';

function ListItem({ index, name, lastName, document, type, onClick }) {
    return (
        <ItemContainer type={type}>
            <Item>{index}- {lastName} {name} - {document}</Item>
            <ListButton onClick={onClick}>{type !== 'reload' ? 'X' : <RxReload style={{ fontSize: '24px' }} size={24} />}</ListButton>
        </ItemContainer>
    )
}


function Scrolleable({ cardInfo }) {
    return (
        <Container>
            <ScrollTable>
                {cardInfo?.header &&
                    <SHeader>
                        {cardInfo?.header?.title}
                    </SHeader>}
                {cardInfo?.list &&
                    <SBody background={cardInfo?.background ?? "gray"}>
                        <List>
                            {cardInfo?.list?.map((student, index) => (
                                <ListItem key={index}
                                    index={index + 1}
                                    name={student.name}
                                    lastName={student.lastName}
                                    document={student.document}
                                    type={student.status}
                                    onClick={() => console.log(`${student.name} 'seleccionado'`)} />
                            ))}
                        </List>
                    </SBody>}
                <SForm onSubmit={cardInfo?.onSubmit ?? null} >
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
            </ScrollTable>
        </Container>
    )
}

export default Scrolleable; 