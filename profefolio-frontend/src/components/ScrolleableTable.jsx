import React from 'react';
import { SCard, SBody, SHeader, SCol, SForm, Select } from "./componentsStyles/StyledScrolleableList";
import Tabla from './Tabla';
import { useNavigate } from 'react-router';
import TextButton from './TextButton';

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
                    </SBody>}
                <SForm onSubmit={cardInfo?.onSubmit ?? null}>
                    <span>Agregar alumnos</span>
                    <Select aria-label="Default select" >
                        <option value="" disabled selected>Seleccione un alumno</option>
                        {cardInfo?.options?.map((option) => (
                            <option key={option.value} value={option.value}>
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