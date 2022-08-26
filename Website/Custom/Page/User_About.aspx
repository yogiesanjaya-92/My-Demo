<%@ Page Title="" Language="C#" MasterPageFile="~/Template/PageUser.Master" AutoEventWireup="true" CodeBehind="User_About.aspx.cs" Inherits="FirstDemo.User_About" %>

<asp:Content ID="CntAbout" ContentPlaceHolderID="PlaceHolderMain" runat="server">
    <div id="colorlib-about">
        <div class="container">
            <div class="row row-pb-lg">
                <div class="col-md-6 animate-box">
                    <div class="about-desc">
                        <h3>About Me</h3>
                        <p>I am programmer enthusiast that get used to build web apllication using asp.net web form, cms sitecore and sl server. I also can do the web tracking using Google Tag Manager and Google Analysis. I also still develop my skill by learning java script framework using Node.js</p>
                    </div>
                </div>
                <div class="col-md-6 animate-box">
                    <div class="fancy-collapse-panel">
                        <div class="panel-group" id="accordion" role="tablist" aria-multiselectable="true">
                            <div class="panel panel-default">
                                <div class="panel-heading" role="tab" id="headingOne">
                                    <h4 class="panel-title">
                                        <a data-toggle="collapse" data-parent="#accordion" href="#collapseOne" aria-expanded="true" aria-controls="collapseOne">My Education
                                        </a>
                                    </h4>
                                </div>
                                <div id="collapseOne" class="panel-collapse collapse in" role="tabpanel" aria-labelledby="headingOne">
                                    <div class="panel-body">
                                        <p>I get my master degree in Physics on Sriwijaya University and get used to build some computer-based instructional media.</p>
                                        <p>I get my bachelor degree in Physics Education on Bandung Institute of Technology and get used to build some emebeded system that connected to computer with or without cable.</p>
                                    </div>
                                </div>
                            </div>
                            <div class="panel panel-default">
                                <div class="panel-heading" role="tab" id="headingTwo">
                                    <h4 class="panel-title">
                                        <a class="collapsed" data-toggle="collapse" data-parent="#accordion" href="#collapseTwo" aria-expanded="false" aria-controls="collapseTwo">My Experience
                                        </a>
                                    </h4>
                                </div>
                                <div id="collapseTwo" class="panel-collapse collapse" role="tabpanel" aria-labelledby="headingTwo">
                                    <div class="panel-body">
                                        <p>I used to be a lecture at Sriwijaya University Computer Science Faculty for two semesters and become a member of instrumentation lab.</p>
                                        <p>I am working at PT Xtremax Teknologi Indonesia as Back End Developer to handle maintenance and feature development for website that belong to a ministry in Singapore.</p>
                                    </div>
                                </div>
                            </div>
                            <div class="panel panel-default">
                                <div class="panel-heading" role="tab" id="headingThree">
                                    <h4 class="panel-title">
                                        <a class="collapsed" data-toggle="collapse" data-parent="#accordion" href="#collapseThree" aria-expanded="false" aria-controls="collapseThree">My Personality
                                        </a>
                                    </h4>
                                </div>
                                <div id="collapseThree" class="panel-collapse collapse" role="tabpanel" aria-labelledby="headingThree">
                                    <div class="panel-body">
                                        <p>I am a hardworker who get used to work under pressure. I can work as a team and maintain the teamwork of the team. I also can learn something new individually.</p>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-md-8 col-md-offset-2 text-center colorlib-heading animate-box">
                    <h2>My Previous Work</h2>
                    <p>There are the work that i have done in my current job</p>
                </div>
            </div>
            <div class="row">
                <div class="col-md-4 animate-box">
                    <a href="/Html/images/work1.jpg" class="staff-img staff-img2" style="background-image: url(/Html/images/work1.jpg);">
                        <div class="desc-staff">
                            <h3>A-Z Scroll</h3>
                            <span>Staging</span>
                        </div>
                    </a>
                </div>
                <div class="col-md-4 animate-box">
                    <a href="/Html/images/work2.jpg" class="staff-img staff-img2" style="background-image: url(/Html/images/work2.jpg);">
                        <div class="desc-staff">
                            <h3>Child Banner Box </h3>
                            <span>Live</span>
                        </div>
                    </a>
                </div>
                <div class="col-md-4 animate-box">
                    <a href="/Html/images/work3.jpg" class="staff-img staff-img2" style="background-image: url(/Html/images/work3.jpg);">
                        <div class="desc-staff">
                            <h3>Stories Page</h3>
                            <span>Staging</span>
                        </div>
                    </a>
                </div>
            </div>
        </div>
    </div>

    <div id="colorlib-footer">
        <div class="contact-information nomargin">
            <div class="container">
                <div class="row">
                    <div class="col-md-8 col-md-offset-2">
                        <div class="row">
                            <div class="col-md-4 text-center">
                                <div class="info-wrap">
                                    <span class="icon"><i class="icon-location4"></i></span>
                                    <p>
                                        27th Kramat Asem Utara Street, 
                                    <br>
                                        Utan Kayu Utara, Matraman, JakTim
                                    </p>
                                </div>
                            </div>
                            <div class="col-md-4 text-center">
                                <div class="info-wrap">
                                    <span class="icon"><i class="icon-mail"></i></span>
                                    <p>
                                        <a href="#">yogie.sanjaya@ymail.com</a>
                                    </p>
                                </div>
                            </div>
                            <div class="col-md-4 text-center">
                                <div class="info-wrap">
                                    <span class="icon"><i class="icon-world"></i></span>
                                    <p>
                                        <a href="#">yosadev.somee.com</a>
                                        <br>
                                        <a href="#">yogiesanjaya.blogspot.com</a>
                                    </p>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
