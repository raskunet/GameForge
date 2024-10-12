﻿// <auto-generated />
using System;
using System.Collections.Generic;
using GameForge.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace GameForge.Migrations
{
    [DbContext(typeof(GameForgeContext))]
    [Migration("20241012105319_CreateModelForums-1.0")]
    partial class CreateModelForums10
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("GameForge.Models.Answer", b =>
                {
                    b.Property<int>("QuestionID")
                        .HasColumnType("integer");

                    b.Property<int>("UserID")
                        .HasColumnType("integer");

                    b.Property<string>("AnswerText")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("Downvotes")
                        .HasColumnType("integer");

                    b.Property<int>("Upvotes")
                        .HasColumnType("integer");

                    b.HasKey("QuestionID", "UserID");

                    b.HasIndex("UserID", "QuestionID");

                    b.ToTable("Answer");
                });

            modelBuilder.Entity("GameForge.Models.Question", b =>
                {
                    b.Property<int>("QuestionID")
                        .HasColumnType("integer");

                    b.Property<int>("AuthorID")
                        .HasColumnType("integer");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("Downvotes")
                        .HasColumnType("integer");

                    b.Property<int>("LatestAnswerID")
                        .HasColumnType("integer");

                    b.Property<DateTime>("LatestAnswerTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("NumberOfAnswers")
                        .HasColumnType("integer");

                    b.Property<string>("QuestionText")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Upvotes")
                        .HasColumnType("integer");

                    b.HasKey("QuestionID", "AuthorID");

                    b.HasIndex("AuthorID");

                    b.ToTable("Question");
                });

            modelBuilder.Entity("GameForge.Models.ThreadTopic", b =>
                {
                    b.Property<int>("ThreadTopicID")
                        .HasColumnType("integer");

                    b.Property<int>("UserID")
                        .HasColumnType("integer");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("LatestReplyID")
                        .HasColumnType("integer");

                    b.Property<DateTime>("LatestReplyTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Message")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("NumberOfReplies")
                        .HasColumnType("integer");

                    b.Property<List<string>>("Tag")
                        .IsRequired()
                        .HasColumnType("text[]");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("ThreadTopicID", "UserID");

                    b.HasIndex("UserID");

                    b.ToTable("ThreadTopic");
                });

            modelBuilder.Entity("GameForge.Models.ThreadTopicReply", b =>
                {
                    b.Property<int>("ThreadTopicID")
                        .HasColumnType("integer");

                    b.Property<int>("UserID")
                        .HasColumnType("integer");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Message")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("ThreadTopicID", "UserID");

                    b.HasIndex("UserID");

                    b.ToTable("ThreadTopicReplies");
                });

            modelBuilder.Entity("GameForge.Models.User", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("ID"));

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("ID");

                    b.ToTable("User");
                });

            modelBuilder.Entity("GameForge.Models.Answer", b =>
                {
                    b.HasOne("GameForge.Models.User", "User")
                        .WithMany("Answers")
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("GameForge.Models.Question", "Question")
                        .WithMany("Answers")
                        .HasForeignKey("UserID", "QuestionID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Question");

                    b.Navigation("User");
                });

            modelBuilder.Entity("GameForge.Models.Question", b =>
                {
                    b.HasOne("GameForge.Models.User", "User")
                        .WithMany("Questions")
                        .HasForeignKey("AuthorID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("GameForge.Models.ThreadTopic", b =>
                {
                    b.HasOne("GameForge.Models.User", "User")
                        .WithMany("ThreadTopics")
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("GameForge.Models.ThreadTopicReply", b =>
                {
                    b.HasOne("GameForge.Models.User", null)
                        .WithMany("ThreadTopicReplies")
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("GameForge.Models.ThreadTopic", "ThreadTopic")
                        .WithMany("ThreadTopidcReplies")
                        .HasForeignKey("ThreadTopicID", "UserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ThreadTopic");
                });

            modelBuilder.Entity("GameForge.Models.Question", b =>
                {
                    b.Navigation("Answers");
                });

            modelBuilder.Entity("GameForge.Models.ThreadTopic", b =>
                {
                    b.Navigation("ThreadTopidcReplies");
                });

            modelBuilder.Entity("GameForge.Models.User", b =>
                {
                    b.Navigation("Answers");

                    b.Navigation("Questions");

                    b.Navigation("ThreadTopicReplies");

                    b.Navigation("ThreadTopics");
                });
#pragma warning restore 612, 618
        }
    }
}
