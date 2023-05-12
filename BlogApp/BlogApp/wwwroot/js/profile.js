
let personalInfoDiv = document.getElementById('personal-info-block');
let aboutAuthorDiv = document.getElementById('about-author-block');
let personalInfoLi = document.getElementById('personal-info-li');
let aboutAuthorLi = document.getElementById('about-author-li');
function AboutAuthorOnClick(){
    personalInfoLi.classList.remove('chosen');
    aboutAuthorLi.classList.add('chosen');
    personalInfoDiv.style.display = 'none';
    aboutAuthorDiv.style.display = 'flex';
}

function PersonalInformationOnClick(){
    personalInfoLi.classList.add('chosen');
    aboutAuthorLi.classList.remove('chosen');
    personalInfoDiv.style.display = 'flex';
    aboutAuthorDiv.style.display = 'none';
}

let recentActivityDiv = document.getElementById('recent-activity-block');
let blogsDiv = document.getElementById('blogs-block');
let recentActivityLi = document.getElementById('recent-activity-li');
let blogsLi = document.getElementById('blogs-li');

function RecentActivityOnClick(){
    blogsLi.classList.remove('chosen');
    recentActivityLi.classList.add('chosen');
    blogsDiv.style.display = 'none';
    recentActivityDiv.style.display = 'flex';
}

function BlogsOnClick(){
    blogsLi.classList.add('chosen');
    recentActivityLi.classList.remove('chosen');
    blogsDiv.style.display = 'flex';
    recentActivityDiv.style.display = 'none';
}