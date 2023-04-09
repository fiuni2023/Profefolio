import styled from 'styled-components';
import { HiArrowLeft } from 'react-icons/hi'
import {Link } from "react-router-dom";

const StyledNavAdmin = styled.div`
    width: 100%;
    height: 100%;
    background-color: white;
    display: flex;
    background-color: #FDF0D5;
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

const StyledCustomSpan = styled.div`
    padding: 6px;
    margin:0 0 10px 10px;  
    font-size:15px;
    font-weight: bold;
    
`;


const StyleComponentBreadcrumb = ({ nombre }) => {
    return (
        <>
            <StyledNavAdmin>
                <StyledNButtonForSideAdmin>
                    <StyledButtonNavBarAdmin>
                        <Link to="/" className="buttonNavBarAdmin ">
                            
                        <StyledCustomIcon className="glyphicon glyphicon-info-sign" />
                
                            
                            </Link>
                    </StyledButtonNavBarAdmin>

                    <div className="">
                        <div>
                       
                        </div>
                        <StyledCustomSpan>{nombre}</StyledCustomSpan>
                    </div>   
                   
                </StyledNButtonForSideAdmin>
            </StyledNavAdmin>
        </>
    );
};


export default StyleComponentBreadcrumb;