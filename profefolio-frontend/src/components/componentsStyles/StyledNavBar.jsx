import styled from 'styled-components';

const navbarLogoImage = require('../../assets/images/BigLogo.png');

const handleClickable = (clickable) => {
    if (clickable) return 'pointer;'
    else return 'auto !important;'
}

const SNavBar = styled.nav`
    width: 100%;
    height: 100%;
    background-color: white;
    display: flex;
    align-items: center;
    padding: 22px 22px;
    background-color: #F0544F;
    justify-content: space-between;
`;

const SButtonforBar = styled.button`
    outline: none;
    border: none;
    background-color: #F0544F;
    font-size: 30px;
    color: white;
    display: flex; 
    align-items: center; 
`;

const SMainLogo = styled.button`
    outline: none;
    border: none;
    background-color: #F0544F;
    cursor: ${({ clickable }) => handleClickable(clickable)};
    display: flex;
    align-items: center;
    height: 2.1em;
`;

const SmainLogoImg = styled.img`
    height: inherit; 
`;

const SMainUser = styled.div`
    display: flex;
    justify-content: space-evenly;
    align-items: center;
    color: white;
    cursor: pointer;
`;

const SMainName = styled.div`
    color: #ffffff; 
    display: flex; 
    justify-content: space-between;
    align-items: center;
    gap: 1em;
`;

const SUserName = styled.span`
    font-size: 1.5em;
`;

const SSchoolName = styled.div`
    color: #ffffff; 
    font-size: 2em;
`;

const NavLeft = styled.div`
    display: flex; 
`;
const NavCenter = styled.div`
    display: flex; 
`;
const NavRight = styled.div`
    display: flex;
`;

const SIcon = styled.div`
    font-size: 25px; 
`;


export {SIcon, SUserName, NavRight, NavCenter, NavLeft, SNavBar, SButtonforBar, SMainLogo, navbarLogoImage, SmainLogoImg, SMainUser, SMainName, SSchoolName};