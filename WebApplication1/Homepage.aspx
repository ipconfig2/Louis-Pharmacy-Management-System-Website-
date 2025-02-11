<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="Homepage.aspx.cs" Inherits="WebApplication1.Homepage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
   
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container p-5 my-5 text-center border shadow-lg rounded-lg">
        <h1><asp:Label ID="lblWelcome" runat="server" Text="Welcome"></asp:Label></h1>
        <h2><asp:Label ID="Label1" runat="server" Text="Beyond the counter there's a heart dedicated to healing"></asp:Label></h2>
        <asp:Image ID="imgHome" runat="server" ImageUrl="~/home.jpg" AlternateText="Home Image" CssClass="img-fluid rounded" />
        <p>We provide personalized care and pharmacy services to ensure your health and wellness are our priority. Our team is dedicated to helping you achieve a healthier, happier life.</p>
    </div>

    <div class="container p-5 my-5 text-center border shadow-lg rounded-lg">
        <h2 class="font-weight-bold">Our Services</h2>
        <div class="row">
            <div class="col-md-4">
                <h3>Prescription Fulfillment</h3>
                <p>We offer fast and reliable prescription fulfillment with personalized service for each of our customers.</p>
            </div>
            <div class="col-md-4">
                <h3>Consultations</h3>
                <p>Get professional advice on your medications, health concerns, and wellness plans from our experienced team.</p>
            </div>
            <div class="col-md-4">
                <h3>Vaccinations</h3>
                <p>Stay up-to-date with essential vaccines for your health and safety, administered by certified pharmacists.</p>
            </div>
        </div>
    </div>

    <div class="container p-5 my-5 text-center border shadow-lg rounded-lg">
        <h2 class="font-weight-bold">Employee of the Month</h2>
        <asp:Image ID="imgEmployee" runat="server" ImageUrl="~/employee.jpg" AlternateText="Employee of the Month" CssClass="img-fluid rounded-circle shadow-sm" Width="200px" Height="200px" />
        <h3><asp:Label ID="lblEmployeeName" runat="server" Text="Dr. Mcfarlane"></asp:Label></h3>
        <p>Dr. McFarlane has been with us for over 20 years and is known for his exceptional care and dedication to our community. Congratulations, Dr. McFarlane!</p>
    </div>

    <div class="container p-5 my-5 text-center border shadow-lg rounded-lg">
        <h2 class="font-weight-bold">What Our Customers Say</h2>
        <div class="row">
            <div class="col-md-6">
                <blockquote>
                    <p>"The staff at this pharmacy is incredible! They take the time to explain my medications and always make me feel like a priority." – John D.</p>
                </blockquote>
            </div>
            <div class="col-md-6">
                <blockquote>
                    <p>"I can't imagine going anywhere else. They helped me find the right treatments, and I always feel well taken care of." – Sarah P.</p>
                </blockquote>
            </div>
        </div>
    </div>

    <div class="container p-5 my-5 text-center border shadow-lg rounded-lg">
        <h2 class="font-weight-bold">Contact Us</h2>
        <p>If you have any questions or need assistance, feel free to reach out to us. We're here to help!</p>
        <p><strong>Email:</strong> support@LouisPharmacy.com</p>
        <p><strong>Phone:</strong> (800) 123-4567</p>
        <p><a href="mailto:support@LouisPharmacy.com" class="btn btn-primary">Send us an email</a></p>
    </div>
</asp:Content>
