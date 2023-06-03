import styled from 'styled-components';
import { HiArrowLeft } from 'react-icons/hi'
import {Link } from "react-router-dom";

const StyledNavAdmin = styled.div`
    width: 100%;
    background-color: white;
    display: flex;
    align-items: center;
    background-color: #FDF0D5;
    min-height: 40px;
`;

const StyledCustomIcon = styled(HiArrowLeft)`
    color: black !important;
    font-size: 20px;
`;

const StyledNButtonForSideAdmin = styled.div`
    display: flex;
    font-size: 20px;
`;

const StyledButtonNavBarAdmin = styled.button`
    font-weight: bold;
    width: 100%;
    height: 100%;
    outline: none;
    border: none;
    background-color: #FDF0D5;
    font-size: 20px;
    color: black;
`;

const StyledCustomSpan = styled.span`
    margin-left: 2%;
    font-size:15px;
    font-weight: bold;
    
`;


const StyleComponentBreadcrumb = ({ nombre, to = "/" }) => {
    return (
        <>
            <StyledNavAdmin>
                <StyledNButtonForSideAdmin>
                    <StyledButtonNavBarAdmin>
                        <Link to={to} className="buttonNavBarAdmin ">
                            
                            <StyledCustomIcon className="glyphicon glyphicon-info-sign" />
                
                            
                        </Link>
                    </StyledButtonNavBarAdmin>
                    
                   
                </StyledNButtonForSideAdmin>
                <StyledCustomSpan>{nombre}</StyledCustomSpan>
            </StyledNavAdmin>
        </>
    );
};


export default StyleComponentBreadcrumb;