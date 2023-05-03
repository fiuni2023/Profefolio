import styled from "styled-components";

export const AddButton = styled.button`
    width: 50px;
    height: 50px;
    padding: 7px;
    color: white;
    background-color: #F0544F;
    border-radius: 50%;
    position: fixed;
    bottom: 1.5%;
    right: 1%;
    cursor: pointer;
    border: none;
&:hover {
    filter: brightness(0.95);
&:active {
    filter: brightness(0.8);
  }
`;

export const MainContainer =  styled.div`
    display: 'grid',
    gridTemplateRows: '5% 95%',
    width: '100%',
    height: '100vh'
`;

export const TableContainer =  styled.div`
    height: 100%;
    width: 100%;
    padding: 2vh;
`;