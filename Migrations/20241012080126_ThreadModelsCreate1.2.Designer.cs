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
    [Migration("20241012080126_ThreadModelsCreate1.2")]
    partial class ThreadModelsCreate12
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("GameForge.Models.ThreadTopic", b =>
                {
                    b.Property<int>("ThreadTopicID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("ThreadTopicID"));

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

                    b.Property<int>("UserID")
                        .HasColumnType("integer");

                    b.HasKey("ThreadTopicID");

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
                    b.HasOne("GameForge.Models.ThreadTopic", "ThreadTopic")
                        .WithMany("ThreadTopidcReplies")
                        .HasForeignKey("ThreadTopicID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ThreadTopic");
                });

            modelBuilder.Entity("GameForge.Models.ThreadTopic", b =>
                {
                    b.Navigation("ThreadTopidcReplies");
                });

            modelBuilder.Entity("GameForge.Models.User", b =>
                {
                    b.Navigation("ThreadTopics");
                });
#pragma warning restore 612, 618
        }
    }
}
