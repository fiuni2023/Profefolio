import React from 'react'
import { SBody, SCard, SCol, SHeader } from '../../../../../components/componentsStyles/StyledDashComponent'

const CustomCardChart = ({ title, graph }) => {
    return <SCol xs={12} sm={12} md={12} lg={12} style={{ height: "400px" }}>
        <SCard >
            <SHeader background={"gray"}>
                {title}</SHeader>
            <SBody background={"gray"}>
                {graph}
            </SBody>
        </SCard>
    </SCol>
}

export default CustomCardChart