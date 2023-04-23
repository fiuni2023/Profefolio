import React from 'react'
import { SContainer, SRow } from '../../../components/componentsStyles/StyledForm'
import { map } from "lodash";
import { Col } from 'react-bootstrap';
import { STitle } from './ShowsStyled';

const ShowContainer = ({ data = {} }) => {
    const combinaciones = [[12], [6, 6], [12, 6, 6]]; // combinacion de tamanhos de columnas
    return <>
        <div className="principal-container">
            <SContainer>
                <STitle>{data?.title}</STitle>
                <SRow className="srow-showclase">
                    {map(data?.componentes, (e, i) => <Col
                        className="scol-showclase"
                        key={i}
                        md={combinaciones[data?.componentes?.length - 1 >= 0 ? data?.componentes?.length - 1 : 0][i]}
                    >{e}</Col>)
                    }
                </SRow>
            </SContainer>
        </div>
        <style jsx="true">
            {`
                .principal-container{
                    margin-top: 2rem;
                }
                .srow-showclase{
                    flex-wrap: wrap;
                }
                .scol-showclase {
                    box-sizing: border-box;
                    padding: 1rem;
                }
                
            `}
        </style>
    </>
}

export default ShowContainer